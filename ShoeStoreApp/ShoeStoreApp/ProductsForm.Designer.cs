namespace ShoeStoreApp
{
    partial class ProductsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelUser = new Label();
            buttonExit = new Button();
            flowProducts = new FlowLayoutPanel();
            buttonAddProduct = new Button();
            labelSearch = new Label();
            textBoxSearch = new TextBox();
            labelProvider = new Label();
            labelSort = new Label();
            comboBoxProviderFilter = new ComboBox();
            comboBoxSort = new ComboBox();
            buttonOrders = new Button();
            SuspendLayout();
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelUser.Location = new Point(653, 8);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(125, 22);
            labelUser.TabIndex = 0;
            labelUser.Text = "Пользователь";
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.MediumSpringGreen;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonExit.Location = new Point(806, 649);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(321, 35);
            buttonExit.TabIndex = 2;
            buttonExit.Text = "Выход";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // flowProducts
            // 
            flowProducts.AutoScroll = true;
            flowProducts.FlowDirection = FlowDirection.TopDown;
            flowProducts.Location = new Point(12, 93);
            flowProducts.Name = "flowProducts";
            flowProducts.Size = new Size(1115, 550);
            flowProducts.TabIndex = 3;
            flowProducts.WrapContents = false;
            // 
            // buttonAddProduct
            // 
            buttonAddProduct.BackColor = Color.MediumSpringGreen;
            buttonAddProduct.FlatStyle = FlatStyle.Flat;
            buttonAddProduct.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonAddProduct.Location = new Point(12, 8);
            buttonAddProduct.Name = "buttonAddProduct";
            buttonAddProduct.Size = new Size(177, 29);
            buttonAddProduct.TabIndex = 4;
            buttonAddProduct.Text = "Добавить товар";
            buttonAddProduct.UseVisualStyleBackColor = false;
            buttonAddProduct.Click += buttonAddProduct_Click_1;
            // 
            // labelSearch
            // 
            labelSearch.AutoSize = true;
            labelSearch.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelSearch.Location = new Point(23, 59);
            labelSearch.Name = "labelSearch";
            labelSearch.Size = new Size(70, 22);
            labelSearch.TabIndex = 5;
            labelSearch.Text = "Поиск:";
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(99, 59);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(189, 25);
            textBoxSearch.TabIndex = 6;
            // 
            // labelProvider
            // 
            labelProvider.AutoSize = true;
            labelProvider.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelProvider.Location = new Point(307, 62);
            labelProvider.Name = "labelProvider";
            labelProvider.Size = new Size(111, 22);
            labelProvider.TabIndex = 7;
            labelProvider.Text = "Поставщик:";
            // 
            // labelSort
            // 
            labelSort.AutoSize = true;
            labelSort.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelSort.Location = new Point(633, 62);
            labelSort.Name = "labelSort";
            labelSort.Size = new Size(117, 22);
            labelSort.TabIndex = 8;
            labelSort.Text = "Сортировка:";
            // 
            // comboBoxProviderFilter
            // 
            comboBoxProviderFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProviderFilter.Font = new Font("Times New Roman", 12F);
            comboBoxProviderFilter.FormattingEnabled = true;
            comboBoxProviderFilter.Location = new Point(424, 59);
            comboBoxProviderFilter.Name = "comboBoxProviderFilter";
            comboBoxProviderFilter.Size = new Size(198, 30);
            comboBoxProviderFilter.TabIndex = 9;
            // 
            // comboBoxSort
            // 
            comboBoxSort.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSort.Font = new Font("Times New Roman", 12F);
            comboBoxSort.FormattingEnabled = true;
            comboBoxSort.Location = new Point(756, 60);
            comboBoxSort.Name = "comboBoxSort";
            comboBoxSort.Size = new Size(210, 30);
            comboBoxSort.TabIndex = 10;
            // 
            // buttonOrders
            // 
            buttonOrders.BackColor = Color.MediumSpringGreen;
            buttonOrders.FlatStyle = FlatStyle.Flat;
            buttonOrders.Font = new Font("Times New Roman", 12F);
            buttonOrders.Location = new Point(212, 8);
            buttonOrders.Name = "buttonOrders";
            buttonOrders.Size = new Size(170, 29);
            buttonOrders.TabIndex = 11;
            buttonOrders.Text = "Заказы";
            buttonOrders.UseVisualStyleBackColor = false;
            buttonOrders.Click += buttonOrders_Click;
            // 
            // ProductsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1139, 696);
            Controls.Add(buttonOrders);
            Controls.Add(comboBoxSort);
            Controls.Add(comboBoxProviderFilter);
            Controls.Add(labelSort);
            Controls.Add(labelProvider);
            Controls.Add(textBoxSearch);
            Controls.Add(labelSearch);
            Controls.Add(buttonAddProduct);
            Controls.Add(buttonExit);
            Controls.Add(flowProducts);
            Controls.Add(labelUser);
            Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Name = "ProductsForm";
            Text = "Список товаров";
            Load += ProductsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelUser;
        private Button buttonExit;
        private FlowLayoutPanel flowProducts;
        private Button buttonAddProduct;
        private Label labelSearch;
        private TextBox textBoxSearch;
        private Label labelProvider;
        private Label labelSort;
        private ComboBox comboBoxProviderFilter;
        private ComboBox comboBoxSort;
        private Button buttonOrders;
    }
}