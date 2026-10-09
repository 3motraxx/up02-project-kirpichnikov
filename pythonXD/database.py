import sqlite3

from config import DB_PATH


def get_connection():
    if not DB_PATH.exists():
        raise FileNotFoundError(
            f"Не найдена база данных: {DB_PATH}\n"
            "Скопируй db_variant_28.db в папку databases."
        )

    conn = sqlite3.connect(str(DB_PATH))
    conn.row_factory = sqlite3.Row
    return conn


def get_all_products():
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute(
            'SELECT id, категория, название, состав, цена, количество, фото '
            'FROM "Товар" ORDER BY id'
        )
        return [dict(row) for row in cur.fetchall()]
    finally:
        conn.close()


def get_product(product_id):
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute(
            'SELECT id, категория, название, состав, цена, количество, фото '
            'FROM "Товар" WHERE id = ?',
            (product_id,)
        )
        row = cur.fetchone()
        return dict(row) if row else None
    finally:
        conn.close()


def get_product_columns():
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute('PRAGMA table_info("Товар")')
        return [row["name"] for row in cur.fetchall()]
    finally:
        conn.close()


def get_all_orders():
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute(
            'SELECT z.id, z.дата, z.клиент, z.товар_id, z.количество, '
            't.название AS название_товара, t.цена '
            'FROM "Заказ" z '
            'LEFT JOIN "Товар" t ON t.id = z.товар_id '
            'ORDER BY z.id DESC'
        )
        return [dict(row) for row in cur.fetchall()]
    finally:
        conn.close()