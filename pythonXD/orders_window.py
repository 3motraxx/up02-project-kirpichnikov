import tkinter as tk
from tkinter import ttk, messagebox
from styles import COLOR_MAIN_BG, COLOR_SECONDARY_BG, font, FONT_SIZE_TITLE
import order_manager as om

class OrdersWindow:
    def __init__(self, parent, current_user=None):
        self.current_user = current_user
        self.window = tk.Toplevel(parent)
        self.window.title("Список заказов")
        self.window.geometry("720x430")
        self.window.configure(bg=COLOR_MAIN_BG)
        tk.Label(self.window, text="ЗАКАЗЫ", bg=COLOR_SECONDARY_BG,
                 font=font(FONT_SIZE_TITLE, True), pady=12).pack(fill="x")
        self.tree = ttk.Treeview(self.window, columns=("id", "date", "client"),
                                 show="headings")
        for col, title, width in [
            ("id", "№ заказа", 90), ("date", "Дата", 140), ("client", "Клиент", 400)
        ]:
            self.tree.heading(col, text=title)
            self.tree.column(col, width=width, anchor="w")
        self.tree.pack(fill="both", expand=True, padx=12, pady=12)
        self.tree.bind("<Double-1>", self.on_select)
        tk.Button(self.window, text="Обновить", command=self.load_orders).pack(pady=(0, 10))
        self.load_orders()

    def load_orders(self):
        for item in self.tree.get_children():
            self.tree.delete(item)
        try:
            for row in om.get_all_orders():
                self.tree.insert("", "end", values=tuple(row))
        except Exception as exc:
            messagebox.showerror("Ошибка", str(exc), parent=self.window)

    def on_select(self, _event=None):
        selected = self.tree.selection()
        if not selected:
            return
        order_id = self.tree.item(selected[0], "values")[0]
        from order_items_window import OrderItemsWindow
        OrderItemsWindow(self.window, int(order_id))
