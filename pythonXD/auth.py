import sqlite3
from database import get_connection

def get_users():
    conn = get_connection()
    try:
        cur = conn.cursor()
        cur.execute('SELECT * FROM "Пользователь"')
        return [dict(row) for row in cur.fetchall()]
    finally:
        conn.close()

def find_user(login):
    for user in get_users():
        if str(user.get("логин", "")) == login:
            return user
    return None
