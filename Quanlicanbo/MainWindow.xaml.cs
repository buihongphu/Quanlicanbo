using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Quanlicanbo
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

        private void btnKetnoi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-2SVCGMM\SQLEXPRESS;
                                    Database=quanlicanbo;
                                    Integrated Security=SSPI"))
                {
                    connection.Open();
                }
                MessageBox.Show("Mo va dong co so du lieu thanh cong.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi: " + ex.Message);
            }
        }

        private void btnDulieu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<phongban> DanhSachPhongBan = new List<phongban>();
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-2SVCGMM\SQLEXPRESS;Database=quanlicanbo;
                                 Integrated Security=SSPI"))
                using (SqlCommand command =
                new SqlCommand("SELECT phongbanID, tenphongban from phongban; ", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pb = new phongban();
                            pb.phongbanID = reader.GetString(0);
                            pb.tenphongban = reader.GetString(1);
                            DanhSachPhongBan.Add(pb);
                        }
                    }
                }
                MessageBox.Show("Mo va dong co so du lieu thanh cong.");
                Dulieu.ItemsSource = DanhSachPhongBan;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi: " + ex.Message);
            }
        }

        private void tbnThem_Click(object sender, RoutedEventArgs e)
        {
            phongban pb = new phongban();
            pb.phongbanID = tbxPhongBAn.Text;
            pb.tenphongban = tbxTenPhong.Text;
            if (Them_sinh_vien(pb) > 0)
                MessageBox.Show("Du lieu duoc them thanh cong!");
        }
        private int Them_sinh_vien(phongban phongban)
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(@"Server=DESKTOP-2SVCGMM\SQLEXPRESS;Database=quanlicanbo;
        Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("INSERT INTO phongban(phongbanID,tenphongban )" + "VALUES(@phongbanID,@tenphongban );", connection))
                {
                    command.Parameters.Add("phongbanID", SqlDbType.Char, 10).Value =
                        phongban.phongbanID;
                    object dbtenphongban = phongban.tenphongban;
                    if (dbtenphongban == null)
                    {
                        dbtenphongban = DBNull.Value;
                    }

                    command.Parameters.Add("tenphongban", SqlDbType.NVarChar, 30).Value = dbtenphongban;



                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi:" + ex.Message);
                return -1;
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            phongban pb = new phongban();
            pb.phongbanID = tbxPhongBAn.Text;
            if (Xoa_sinh_vien(pb) > 0)
                MessageBox.Show("Du lieu duoc xoa thanh cong!");
        }
        private int Xoa_sinh_vien(phongban sinhvien)
        {

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(@"Server=DESKTOP-2SVCGMM\SQLEXPRESS;Database=quanlicanbo;Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("DELETE FROM phongban " + "WHERE phongbanID = @phongbanID", connection))
                {
                    command.Parameters.Add("phongbanID", SqlDbType.NChar, 10).Value = sinhvien.phongbanID;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi:" + ex.Message);
                return -1;
            }

        }


        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            phongban pb = new phongban();
            pb.phongbanID = tbxPhongBAn.Text;
            pb.tenphongban = tbxTenPhong.Text;

            if (Cap_nhat_sinh_vien(pb) > 0)
                MessageBox.Show("Du lieu duoc cap nhat thanh cong!");

        }
        private int Cap_nhat_sinh_vien(phongban phongban)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-2SVCGMM\SQLEXPRESS;Database=quanlicanbo; Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("UPDATE phongban " + "SET tenphongban = @tenphongban " + "WHERE phongbanID = @phongbanID", connection))
                {
                    command.Parameters.Add("phongbanID", SqlDbType.Char, 10).Value = phongban.phongbanID; command.Parameters.Add("tenphongban", SqlDbType.NVarChar, 30).Value = phongban.tenphongban;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi:" + ex.Message);
                return -1;
            }
        }
    }
}
