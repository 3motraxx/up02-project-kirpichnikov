using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProductCard
{
    public partial class Form1 : Form
    {
        private FlowLayoutPanel catalogPanel = null!;

        public Form1()
        {
            InitializeComponent();

            BuildUI();
            LoadProducts();
        }

        private void BuildUI()
        {
            Text = "Каталог товаров";
            Width = 900;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Styles.COLOR_MAIN_BG;

            SetApplicationIcon();

            Panel header = CreateHeader();

            catalogPanel = new FlowLayoutPanel();

            catalogPanel.Dock = DockStyle.Fill;
            catalogPanel.BackColor = Styles.COLOR_MAIN_BG;
            catalogPanel.FlowDirection = FlowDirection.TopDown;
            catalogPanel.WrapContents = false;
            catalogPanel.AutoScroll = true;
            catalogPanel.Padding = new Padding(10);

            Controls.Add(catalogPanel);
            Controls.Add(header);
        }

        private void SetApplicationIcon()
        {
            string iconPath = Resources.PATH_ICON;

            if (System.IO.File.Exists(iconPath))
            {
                try
                {
                    Icon = new Icon(iconPath);
                }
                catch
                {
                }
            }
        }

        private Panel CreateHeader()
        {
            Panel header = new Panel();

            header.Dock = DockStyle.Top;
            header.Height = 80;
            header.BackColor = Styles.COLOR_SECONDARY_BG;

            PictureBox logo = new PictureBox();

            logo.Width = 60;
            logo.Height = 60;
            logo.Location = new Point(15, 10);
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.BackColor = Color.Transparent;

            Image? logoImage =
                Resources.LoadImageProportional(
                    Resources.PATH_LOGO,
                    new Size(60, 60)
                );

            if (logoImage != null)
            {
                logo.Image = logoImage;
            }

            header.Controls.Add(logo);

            Label title = new Label();

            title.Text = "КАТАЛОГ ТОВАРОВ";
            title.Font = Styles.Font(
                Styles.FONT_SIZE_TITLE,
                true
            );
            title.Dock = DockStyle.Fill;
            title.TextAlign =
                ContentAlignment.MiddleCenter;

            header.Controls.Add(title);

            return header;
        }

        private void LoadProducts()
        {
            object? result = ErrorHandler.SafeCall(
                () => Database.GetAllProducts()
            );

            List<Product> products =
                result as List<Product>
                ?? new List<Product>();

            foreach (Product product in products)
            {
                ErrorHandler.SafeCall(
                    () =>
                    {
                        Panel card =
                            CreateProductCard(product);

                        catalogPanel.Controls.Add(card);

                        return null;
                    }
                );
            }
        }

        // Главная функция создания карточки
        private Panel CreateProductCard(Product product)
        {
            Panel card = CreateCard(product);

            AddProductImage(card, product);
            AddTitle(card, product);
            AddCategory(card, product);
            AddQuantity(card, product);
            AddComposition(card, product);
            AddPrice(card, product);

            // Клик по карточке
            card.Cursor = Cursors.Hand;

            card.Click += (sender, e) =>
            {
                OpenView(product);
            };

            // Клик по элементам внутри карточки
            foreach (Control child in card.Controls)
            {
                child.Cursor = Cursors.Hand;

                child.Click += (sender, e) =>
                {
                    OpenView(product);
                };
            }

            return card;
        }

        // Создание самой карточки
        private Panel CreateCard(Product product)
        {
            Color bgColor =
                GetCardColor(product.Quantity);

            Panel card = new Panel();

            card.Width = 820;
            card.Height = 160;
            card.BackColor = bgColor;
            card.BorderStyle =
                BorderStyle.FixedSingle;
            card.Margin = new Padding(5);

            return card;
        }

        // Изображение товара
        private void AddProductImage(
            Panel card,
            Product product)
        {
            PictureBox picture = new PictureBox();

            picture.Width = 100;
            picture.Height = 100;
            picture.Location =
                new Point(15, 25);

            picture.BackColor =
                card.BackColor;

            picture.SizeMode =
                PictureBoxSizeMode.StretchImage;

            try
            {
                string imagePath =
                    product.ImagePath;

                bool isPlaceholder =
                    string.IsNullOrWhiteSpace(imagePath) ||
                    !System.IO.File.Exists(imagePath);

                if (isPlaceholder)
                {
                    throw new Exception(
                        "Нет изображения"
                    );
                }

                picture.Image =
                    Resources.GetProductImage(
                        imagePath,
                        new Size(100, 100)
                    );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"[DEBUG] Ошибка загрузки фото: {ex.Message}"
                );

                Label noPhoto = new Label();

                noPhoto.Text = "[НЕТ ФОТО]";
                noPhoto.BackColor =
                    card.BackColor;
                noPhoto.AutoSize = true;
                noPhoto.Location =
                    new Point(15, 40);

                picture.Controls.Add(noPhoto);
            }

            card.Controls.Add(picture);
        }

        // Название товара
        private void AddTitle(
            Panel card,
            Product product)
        {
            string name =
                string.IsNullOrWhiteSpace(
                    product.Name
                )
                    ? "[Без названия]"
                    : product.Name;

            Label title = new Label();

            title.Text =
                $"Цветы | {name}";

            title.Font = Styles.Font(
                Styles.FONT_SIZE_HEADER,
                true
            );

            title.Location =
                new Point(135, 15);

            title.AutoSize = true;
            title.BackColor =
                card.BackColor;

            card.Controls.Add(title);
        }

        // Категория
        private void AddCategory(
            Panel card,
            Product product)
        {
            string category =
                string.IsNullOrWhiteSpace(
                    product.Category
                )
                    ? "[Без категории]"
                    : product.Category;

            Label categoryLabel = new Label();

            categoryLabel.Text =
                $"Категория: {category}";

            categoryLabel.Font =
                Styles.Font(
                    Styles.FONT_SIZE_NORMAL
                );

            categoryLabel.Location =
                new Point(135, 50);

            categoryLabel.AutoSize = true;
            categoryLabel.BackColor =
                card.BackColor;

            card.Controls.Add(
                categoryLabel
            );
        }

        // Количество
        private void AddQuantity(
            Panel card,
            Product product)
        {
            string indicator =
                Indicator(product.Quantity);

            Label quantity = new Label();

            quantity.Text =
                $"Количество: {indicator} ({product.Quantity})";

            quantity.Font =
                Styles.Font(
                    Styles.FONT_SIZE_NORMAL
                );

            quantity.Location =
                new Point(135, 75);

            quantity.AutoSize = true;
            quantity.BackColor =
                card.BackColor;

            card.Controls.Add(quantity);
        }

        // Состав
        private void AddComposition(
            Panel card,
            Product product)
        {
            Label composition = new Label();

            composition.Text =
                $"Состав: {product.Composition}";

            composition.Font =
                Styles.Font(
                    Styles.FONT_SIZE_NORMAL
                );

            composition.Location =
                new Point(135, 100);

            composition.AutoSize = true;
            composition.BackColor =
                card.BackColor;

            card.Controls.Add(composition);
        }

        // Цена
        private void AddPrice(
            Panel card,
            Product product)
        {
            decimal price =
                product.Price;

            Label priceLabel =
                new Label();

            priceLabel.Text =
                $"{price:F2} руб.";

            priceLabel.Font =
                Styles.Font(
                    Styles.FONT_SIZE_HEADER,
                    true
                );

            priceLabel.Location =
                new Point(650, 60);

            priceLabel.AutoSize = true;
            priceLabel.BackColor =
                card.BackColor;

            card.Controls.Add(
                priceLabel
            );
        }

        // Индикатор количества
        private string Indicator(int qty)
        {
            return qty > 5
                ? "много"
                : "мало";
        }

        // Цвет карточки
        private Color GetCardColor(int qty)
        {
            return qty <= 5
                ? Styles.COLOR_HIGHLIGHT
                : Styles.COLOR_MAIN_BG;
        }

        // Открытие карточки товара
        private void OpenView(Product product)
        {
            ViewForm viewForm =
                new ViewForm(
                    product,
                    AddToOrder
                );

            viewForm.ShowDialog(this);
        }

        // Добавление товара в заказ
        private void AddToOrder(Product product)
        {
            MessageBox.Show(
                $"Товар «{product.Name}» добавлен в заказ.",
                "Успех",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}