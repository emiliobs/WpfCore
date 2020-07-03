using System.Linq;
using System.Windows;
using WpfCore.Data;
using WpfCore.Models;

namespace WpfCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //using (var db = new AppDbContext())
            //{
            //    var studentData = db.Students.ToList();
            //    StudentList.Items.Add(studentData);
            //}

            PopulateStudentData();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var studentName = strName.Text;
            var studentAddress = strAddress.Text;

            using (var db = new AppDbContext())
            {
                db.Students.Add(new Student
                {
                    Name = studentName,
                    Address = studentAddress,
                });

                //var student = new Student 
                //{
                //    Name = studentName,
                //    Address = studentAddress,
                //};

                //db.Students.Add(student);
                db.SaveChanges();
                StudentList.Items.Clear();
                PopulateStudentData();
            }
        }

        private void PopulateStudentData()
        {
            using (var db = new AppDbContext())
            {
                var studentData = db.Students.ToList();
                foreach (var student in studentData)
                {
                    StudentList.Items.Add(student);

                }
            }
        }
    }
}
