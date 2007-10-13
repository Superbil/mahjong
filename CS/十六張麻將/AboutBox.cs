using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace MAHJONG
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            //  初始化 AboutBox，以便從組件資訊顯示產品資訊。
            //  透過下列方法，變更應用程式的組件資訊設定:
            //  - 專案->屬性->應用程式->組件資訊
            //  - AssemblyInfo.cs
            this.Text = String.Format("關於 {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("版本 {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region 組件屬性存取子

        public string AssemblyTitle
        {
            get
            {
                // 取得這個組件的所有 Title 屬性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // 如果至少有一個 Title 屬性
                if (attributes.Length > 0)
                {
                    // 選取第一個屬性
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // 如果不是空字串，則傳回字串
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // 如果沒有 Title 屬性，或 Title 屬性為空字串，則傳回 .exe 名稱
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // 取得這個組件的所有 Description 屬性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // 如果沒有任何 Description 屬性，則傳回空字串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Description 屬性，則傳回其值
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // 取得這個組件的所有 Product 屬性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // 如果沒有任何 Product 屬性，則傳回空字串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Product 屬性，則傳回其值
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // 取得這個組件的所有 Copyright 屬性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // 如果沒有任何 Copyright 屬性，則傳回空字串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Copyright 屬性，則傳回其值
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // 取得這個組件的所有 Company 屬性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // 如果沒有任何 Company 屬性，則傳回空字串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Company 屬性，則傳回其值
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
