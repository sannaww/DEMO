namespace ShoeStoreApp
{
    partial class OrderEditForm
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
            label1 = new Label();
            textBoxArticle = new TextBox();
            label2 = new Label();
            comboBoxStatus = new ComboBox();
            label3 = new Label();
            comboBoxPoint = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            dateTimePickerCreate = new DateTimePicker();
            dateTimePickerDelivery = new DateTimePicker();
            buttonSave = new Button();
            buttonExit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 50);
            label1.Name = "label1";
            label1.Size = new Size(143, 22);
            label1.TabIndex = 0;
            label1.Text = "Артикул товара";
            // 
            // textBoxArticle
            // 
            textBoxArticle.Location = new Point(206, 47);
            textBoxArticle.Name = "textBoxArticle";
            textBoxArticle.Size = new Size(237, 30);
            textBoxArticle.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 113);
            label2.Name = "label2";
            label2.Size = new Size(125, 22);
            label2.TabIndex = 2;
            label2.Text = "Статус заказа";
            // 
            // comboBoxStatus
            // 
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStatus.FormattingEnabled = true;
            comboBoxStatus.Location = new Point(206, 110);
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(237, 30);
            comboBoxStatus.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(34, 175);
            label3.Name = "label3";
            label3.Size = new Size(129, 22);
            label3.TabIndex = 4;
            label3.Text = "Пункт выдачи";
            // 
            // comboBoxPoint
            // 
            comboBoxPoint.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPoint.FormattingEnabled = true;
            comboBoxPoint.Location = new Point(206, 175);
            comboBoxPoint.Name = "comboBoxPoint";
            comboBoxPoint.Size = new Size(234, 30);
            comboBoxPoint.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 241);
            label4.Name = "label4";
            label4.Size = new Size(108, 22);
            label4.TabIndex = 6;
            label4.Text = "Дата заказа";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(34, 303);
            label5.Name = "label5";
            label5.Size = new Size(132, 22);
            label5.TabIndex = 7;
            label5.Text = "Дата доставки";
            // 
            // dateTimePickerCreate
            // 
            dateTimePickerCreate.Format = DateTimePickerFormat.Short;
            dateTimePickerCreate.Location = new Point(206, 241);
            dateTimePickerCreate.Name = "dateTimePickerCreate";
            dateTimePickerCreate.Size = new Size(234, 30);
            dateTimePickerCreate.TabIndex = 8;
            // 
            // dateTimePickerDelivery
            // 
            dateTimePickerDelivery.Format = DateTimePickerFormat.Short;
            dateTimePickerDelivery.Location = new Point(206, 297);
            dateTimePickerDelivery.Name = "dateTimePickerDelivery";
            dateTimePickerDelivery.Size = new Size(234, 30);
            dateTimePickerDelivery.TabIndex = 9;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.MediumSpringGreen;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Location = new Point(34, 411);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(163, 54);
            buttonSave.TabIndex = 10;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.MediumSpringGreen;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Location = new Point(264, 411);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(176, 54);
            buttonExit.TabIndex = 11;
            buttonExit.Text = "Назад";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonBack_Click;
            // 
            // OrderEditForm
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 557);
            Controls.Add(buttonExit);
            Controls.Add(buttonSave);
            Controls.Add(dateTimePickerDelivery);
            Controls.Add(dateTimePickerCreate);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(comboBoxPoint);
            Controls.Add(label3);
            Controls.Add(comboBoxStatus);
            Controls.Add(label2);
            Controls.Add(textBoxArticle);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 3, 4, 3);
            Name = "OrderEditForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Добавление заказа";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxArticle;
        private Label label2;
        private ComboBox comboBoxStatus;
        private Label label3;
        private ComboBox comboBoxPoint;
        private Label label4;
        private Label label5;
        private DateTimePicker dateTimePickerCreate;
        private DateTimePicker dateTimePickerDelivery;
        private Button buttonSave;
        private Button buttonExit;
    }
}