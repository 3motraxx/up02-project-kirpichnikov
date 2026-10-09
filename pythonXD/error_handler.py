from tkinter import messagebox

def safe_call(func, *args, title="Ошибка", **kwargs):
    try:
        return func(*args, **kwargs)
    except Exception as exc:
        messagebox.showerror(title, str(exc))
        return None

def validate_positive_int(value, field_name="Количество"):
    try:
        number = int(value)
    except (TypeError, ValueError):
        return False, 0, f"{field_name} должно быть целым числом."
    if number <= 0:
        return False, 0, f"{field_name} должно быть больше нуля."
    return True, number, ""
