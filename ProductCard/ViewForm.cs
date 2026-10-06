using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProductCard
{
    public class ViewForm : Form
    {
        private readonly Product product;
        private readonly Action<Product>? onAddToOrder;

        public ViewForm(
            Product product,
            Action<Product>? onAddToOrder = null)
        {
            this.product = product;
            this.onAddToOrder = onAddToOrder;

            Text = $"Просмотр — {product.Name}";
            Width = 700;
            Height = 600;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Styles.COLOR_MAIN_BG;

            BuildUI();
        }

        private void BuildUI()
        {
            // Шапка
            Panel header = new Panel();

            header.Dock = DockStyle.Top;
            header.Height = 60;
            header.BackColor = Styles.COLOR_SECONDARY_BG;

            Label headerLabel = new Label();

            headerLabel.Text = "КАРТОЧКА ТОВАРА";
            headerLabel.Font = Styles.Font(
                Styles.FONT_SIZE_TITLE,
                true
            );
            headerLabel.Dock = DockStyle.Fill;
            headerLabel.TextAlign =
                ContentAlignment.MiddleCenter;
            headerLabel.BackColor =
                Styles.COLOR_SECONDARY_BG;

            header.Controls.Add(headerLabel);
            Controls.Add(header);

            // Основная область
            Panel main = new Panel();

            main.Dock = DockStyle.Fill;
            main.BackColor = Styles.COLOR_MAIN_BG;
            main.Padding = new Padding(20);

            Controls.Add(main);

            // Изображение
            Panel imgFrame = new Panel();

            imgFrame.Width = 220;
            imgFrame.Dock = DockStyle.Left;
            imgFrame.BackColor =
                Styles.COLOR_MAIN_BG;

            PictureBox picture = new PictureBox();

            picture.Width = 200;
            picture.Height = 200;
            picture.Location = new Point(10, 20);
            picture.SizeMode =
                PictureBoxSizeMode.StretchImage;
            picture.BackColor =
                Styles.COLOR_MAIN_BG;

            picture.Image =
                Resources.GetProductImage(
                    product.ImagePath,
                    new Size(200, 200)
                );

            imgFrame.Controls.Add(picture);
            main.Controls.Add(imgFrame);

            // Информация
            Panel infoFrame = new Panel();

            infoFrame.Dock = DockStyle.Fill;
            infoFrame.BackColor =
                Styles.COLOR_MAIN_BG;
            infoFrame.Padding =
                new Padding(20, 20, 10, 10);

            main.Controls.Add(infoFrame);

            AddField(
                infoFrame,
                "Наименование",
                product.Name
            );

            AddField(
                infoFrame,
                "Категория",
                product.Category
            );

            AddField(
                infoFrame,
                "Состав",
                product.Composition
            );

            AddField(
                infoFrame,
                "Цена",
                $"{product.Price:F2} руб."
            );

            AddField(
                infoFrame,
                "Количество",
                product.Quantity
            );

            // Кнопки
            Panel btnFrame = new Panel();

            btnFrame.Dock = DockStyle.Bottom;
            btnFrame.Height = 60;
            btnFrame.BackColor =
                Styles.COLOR_MAIN_BG;

            Button addButton = new Button();

            addButton.Text = "Добавить в заказ";
            addButton.Font = Styles.Font(
                Styles.FONT_SIZE_NORMAL
            );
            addButton.BackColor =
                Styles.COLOR_ACCENT;
            addButton.ForeColor = Color.White;
            addButton.Width = 180;
            addButton.Height = 40;
            addButton.Location =
                new Point(20, 10);

            addButton.Click += AddToOrder;

            Button backButton = new Button();

            backButton.Text = "Назад";
            backButton.Font = Styles.Font(
                Styles.FONT_SIZE_NORMAL
            );
            backButton.BackColor =
                Styles.COLOR_ACCENT;
            backButton.ForeColor = Color.White;
            backButton.Width = 120;
            backButton.Height = 40;
            backButton.Location =
                new Point(215, 10);

            backButton.Click += (sender, e) =>
            {
                Close();
            };

            btnFrame.Controls.Add(addButton);
            btnFrame.Controls.Add(backButton);

            Controls.Add(btnFrame);
        }

        private void AddField(
            Panel parent,
            string label,
            object value)
        {
            Panel row = new Panel();

            row.Height = 35;
            row.Dock = DockStyle.Top;
            row.BackColor =
                Styles.COLOR_MAIN_BG;

            Label labelControl = new Label();

            labelControl.Text = label + ":";
            labelControl.Font = Styles.Font(
                Styles.FONT_SIZE_NORMAL,
                true
            );
            labelControl.Width = 120;
            labelControl.Dock =
                DockStyle.Left;
            labelControl.TextAlign =
                ContentAlignment.MiddleLeft;
            labelControl.BackColor =
                Styles.COLOR_MAIN_BG;

            Label valueControl = new Label();

            valueControl.Text =
                Convert.ToString(value) ?? "";

            valueControl.Font = Styles.Font(
                Styles.FONT_SIZE_NORMAL
            );

            valueControl.Dock =
                DockStyle.Fill;

            valueControl.TextAlign =
                ContentAlignment.MiddleLeft;

            valueControl.BackColor =
                Styles.COLOR_MAIN_BG;

            row.Controls.Add(valueControl);
            row.Controls.Add(labelControl);

            parent.Controls.Add(row);
        }

        private void AddToOrder(
    object? sender,
    EventArgs e)
        {
            if (onAddToOrder == null)
            {
                MessageBox.Show(
                    "Функция в разработке",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            if (product == null)
            {
                MessageBox.Show(
                    "Товар не выбран",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            try
            {
                onAddToOrder(product);

                MessageBox.Show(
                    "Товар добавлен в заказ",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось добавить товар:\n{ex.Message}",
                    "Ошибка заказа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}