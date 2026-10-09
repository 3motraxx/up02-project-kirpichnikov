from pathlib import Path

BASE_DIR = Path(__file__).resolve().parent
APP_TITLE = "Каталог товаров — управление заказами"


DB_CANDIDATES = [
    BASE_DIR / "databases" / "db_variant_28.db",
    BASE_DIR / "db_variant_28.db",
]
DB_PATH = next((p for p in DB_CANDIDATES if p.exists()), DB_CANDIDATES[0])

RESOURCES_DIR = BASE_DIR / "resources"
PATH_PICTURE = RESOURCES_DIR / "picture.png"
PATH_LOGO = RESOURCES_DIR / "logo.png"
PATH_ICON = RESOURCES_DIR / "icon.ico"

DEFAULT_CLIENT = "Иванов Иван Иванович"
