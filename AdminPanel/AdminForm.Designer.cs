
namespace AdminPanel
{
    partial class AdminForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.btnBan = new System.Windows.Forms.Button();
            this.btnRole = new System.Windows.Forms.Button();
            this.lbRoles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(12, 12);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(274, 420);
            this.lbUsers.TabIndex = 0;
            // 
            // btnBan
            // 
            this.btnBan.Location = new System.Drawing.Point(292, 207);
            this.btnBan.Name = "btnBan";
            this.btnBan.Size = new System.Drawing.Size(120, 23);
            this.btnBan.TabIndex = 1;
            this.btnBan.Text = "Ban";
            this.btnBan.UseVisualStyleBackColor = true;
            this.btnBan.Click += new System.EventHandler(this.btnBan_Click);
            // 
            // btnRole
            // 
            this.btnRole.Location = new System.Drawing.Point(292, 178);
            this.btnRole.Name = "btnRole";
            this.btnRole.Size = new System.Drawing.Size(120, 23);
            this.btnRole.TabIndex = 2;
            this.btnRole.Text = "Give role";
            this.btnRole.UseVisualStyleBackColor = true;
            this.btnRole.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbRoles
            // 
            this.lbRoles.FormattingEnabled = true;
            this.lbRoles.Location = new System.Drawing.Point(292, 12);
            this.lbRoles.Name = "lbRoles";
            this.lbRoles.Size = new System.Drawing.Size(120, 160);
            this.lbRoles.TabIndex = 3;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbRoles);
            this.Controls.Add(this.btnRole);
            this.Controls.Add(this.btnBan);
            this.Controls.Add(this.lbUsers);
            this.Name = "AdminForm";
            this.Text = "Panel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Button btnBan;
        private System.Windows.Forms.Button btnRole;
        private System.Windows.Forms.ListBox lbRoles;
    }
}

