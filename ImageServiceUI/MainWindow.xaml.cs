using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageServiceUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LogGrid.ColumnCount = 4;
            LogGrid.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            LogGrid.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            LogGrid.Columns[0].Name = "Recipe";
            LogGrid.Columns[1].Name = "Category";
            LogGrid.Columns[2].Name = "Main Ingredients";
            LogGrid.Columns[3].Name = "Rating";

            // Populate the rows.
            string[] row1 = new string[] { "Meatloaf", "Main Dish", "ground beef",
           "**" };
            string[] row2 = new string[] { "Key Lime Pie", "Dessert",
           "lime juice, evaporated milk", "****" };
            string[] row3 = new string[] { "Orange-Salsa Pork Chops", "Main Dish",
           "pork chops, salsa, orange juice", "****" };
            string[] row4 = new string[] { "Black Bean and Rice Salad", "Salad",
           "black beans, brown rice", "****" };
            string[] row5 = new string[] { "Chocolate Cheesecake", "Dessert",
           "cream cheese", "***" };
            string[] row6 = new string[] { "Black Bean Dip", "Appetizer",
           "black beans, sour cream", "***" };
            object[] rows = new object[] { row1, row2, row3, row4, row5, row6 };

            foreach (string[] rowArray in rows)
            {
                LogGrid.Rows.Add(rowArray);
            }
        }
    }
}
