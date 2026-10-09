import sqlite3
from datetime import datetime
from config import DB_PATH


def get_connection():
    if not DB_PATH.exists():
        raise FileNotFoundError(f"Не найдена база данных: {DB_PATH}")
    return sqlite3.connect(str(DB_PATH))


def _columns(cur, table):
    cur.execute(f'PRAGMA table_info("{table}")')
    return {row[1] for row in cur.fetchall()}


def get_product_quantity(product_id):
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute('SELECT количество FROM "Товар" WHERE id = ?', (product_id,))
        row = cur.fetchone()
        return int(row[0]) if row and row[0] is not None else 0
    finally:
        conn.close()


def update_product_quantity(product_id, new_quantity):
    if new_quantity < 0:
        raise ValueError("Количество не может быть отрицательным.")
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute('UPDATE "Товар" SET количество = ? WHERE id = ?', (new_quantity, product_id))
        if cur.rowcount == 0:
            raise ValueError(f"Товар с id={product_id} не найден.")
        conn.commit()
    except Exception:
        conn.rollback()
        raise
    finally:
        conn.close()


def add_order_to_db(client, product_id, quantity=1):
    if int(quantity) <= 0:
        raise ValueError("Количество должно быть больше нуля.")
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute('SELECT количество FROM "Товар" WHERE id = ?', (product_id,))
        row = cur.fetchone()
        if not row:
            raise ValueError("Товар не найден.")
        stock = int(row[0] or 0)
        if stock < int(quantity):
            raise ValueError("Недостаточно товара на складе.")
        date = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        cur.execute(
            'INSERT INTO "Заказ" (дата, клиент, товар_id, количество) VALUES (?, ?, ?, ?)',
            (date, client, product_id, int(quantity))
        )
        order_id = cur.lastrowid
        cur.execute('UPDATE "Товар" SET количество = количество - ? WHERE id = ?',
                    (int(quantity), product_id))
        conn.commit()
        return int(order_id)
    except Exception:
        conn.rollback()
        raise
    finally:
        conn.close()


def get_last_order_id():
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute('SELECT MAX(id) FROM "Заказ"')
        row = cur.fetchone()
        return row[0] if row else None
    finally:
        conn.close()


def get_all_orders():
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute('SELECT id, дата, клиент FROM "Заказ" ORDER BY id DESC')
        return cur.fetchall()
    finally:
        conn.close()


def get_order_items(order_id):
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute(
            'SELECT z.id, t.название, t.категория, t.состав, '
            'z.количество, t.цена FROM "Заказ" z '
            'LEFT JOIN "Товар" t ON z.товар_id = t.id WHERE z.id = ?',
            (order_id,)
        )
        return cur.fetchall()
    finally:
        conn.close()
