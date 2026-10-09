import tkinter as tk
from tkinter import ttk, messagebox
from styles import COLOR_MAIN_BG, COLOR_SECONDARY_BG, font, FONT_SIZE_TITLE
import order_manager as om

class OrderItemsWindow:
    def __init__(self, parent, order_id, current_user=None):
        self.order_id = order_id
        self.current_user = current_user
        self.window = tk.Toplevel(parent)
        self.window.title(f"Состав заказа №{order_id}")
        self.window.geometry("800x400")
        self.window.configure(bg=COLOR_MAIN_BG)
        tk.Label(self.window, text=f"СОСТАВ ЗАКАЗА №{order_id}",
                 bg=COLOR_SECONDARY_BG, font=font(FONT_SIZE_TITLE, True),
                 pady=12).pack(fill="x")
        cols = ("id", "name", "category", "composition", "quantity", "price")
        self.tree = ttk.Treeview(self.window, columns=cols, show="headings")
        labels = {
            "id": "ID заказа", "name": "Товар", "category": "Категория",
            "composition": "Состав", "quantity": "Количество", "price": "Цена"
        }
        widths = (85, 170, 120, 180, 110, 100)
        for col, width in zip(cols, widths):
            self.tree.heading(col, text=labels[col])
            self.tree.column(col, width=width, anchor="w")
        self.tree.pack(fill="both", expand=True, padx=12, pady=12)
        self.load_items()

    def load_items(self):
        try:
            for row in om.get_order_items(self.order_id):
                self.tree.insert("", "end", values=tuple(row))
        except Exception as exc:
            messagebox.showerror("Ошибка", str(exc), parent=self.window)
