using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvantCard.Model;
using MySql.Data.MySqlClient;

namespace KvantCard.ViewModel
{
    public class DatabaseHelper : MySQL
    {
        public void GetStudents(Db db)
        {
            var students = db.Students.Include("Parents");
        }


        public static List<Student> AllStudents()
        {
            /*
             * Выводится:
             *    0 - 7 данные о студенте
             *    8 - наставник
             *    9 - квантум
             *   10 - уровень
             *   11 - группа
             */
            List<Student> array = new List<Student>();
            string sql = "SELECT s.`id`, s.`last_name`, s.`first_name`, s.`middle_name`, " +
                " DATE_FORMAT(s.birth_date, '%d.%m.%Y') as `birth_date`, " +
                " ifnull(s.current_program_id, 1) as programId," +
                " TIMESTAMPDIFF(YEAR, s.birth_date, CURDATE()) AS age, " +
                " s.parent1_id, kmh.id, pr.kvantum_id, pr.level_id, pr.group_id " +
                " FROM `Student` s " +
                " join program pr on s.current_program_id = pr.id " +
                " join kvantum_mentors_hist kmh on pr.kvantum_id = kmh.kvantum_id ";
            if (OpenConnect())
            {
                using (MySqlCommand mc =
                    new MySqlCommand(
                    sql, connection))
                {
                    using (MySqlDataReader dr =
                        mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //DateTime.ParseExact("dd.mm.yyyy", dr.GetString(4), CultureInfo.GetCultureInfo("ru-RU"),
                            array.Add(new Student
                            {
                                ID = dr.GetInt32(0),
                                LastName = dr.GetString(1),
                                FirstName = dr.GetString(2),
                                MiddleName = dr.GetString(3),
                                BirthDate = DateTime.ParseExact(
                                    dr.GetString(4),
                                    "dd.mm.yyyy",
                                    CultureInfo.GetCultureInfo("ru-RU")
                                    ),
                                ProgramID = dr.GetInt32(5),
                                Age = dr.GetInt32(6),
                                Parent1ID = dr.GetInt32(7),
                                MentorID = dr.GetInt32(8),
                                KvantumID = dr.GetInt32(9),
                                LevelID = dr.GetInt32(10),
                                GroupID = dr.GetInt32(11)
                            });
                        }
                    }
                }
                CloseConnect();
            }
            return array;
        }

        public static List<DictionaryItem> LoadDict(string tableName)
        {
            List<DictionaryItem> array = new List<DictionaryItem>();
            string sql = "SELECT id, title FROM " + tableName;
            if (OpenConnect())
            {
                using (MySqlCommand mc =
                    new MySqlCommand(
                    sql, connection))
                {
                    using (MySqlDataReader dr =
                        mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //DateTime.ParseExact("dd.mm.yyyy", dr.GetString(4), CultureInfo.GetCultureInfo("ru-RU"),
                            array.Add(new DictionaryItem
                            {
                                ID = dr.GetInt32(0),
                                Title = dr.GetString(1)
                            });
                        }
                    }
                }
                CloseConnect();
            }
            return array;
        }

        public static bool Insert<T>(T item)
        {
            bool result = false;
            String sql = "";
            using (MySqlCommand mc =
                new MySqlCommand(
                sql, connection))
            {
                //TODO: Сделать добавление записи
                result = true;
            }

            return result;
        }

        public static bool Update<T>(T item)
        {
            bool result = false;
            String sql = "";
            using (MySqlCommand mc =
                new MySqlCommand(
                sql, connection))
            {
                //TODO: Сделать обновление записи
                result = true;
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;
            String sql = "";
            using (MySqlCommand mc =
                new MySqlCommand(
                sql, connection))
            {
                //TODO: Сделать удаление записи
                result = true;
            }

            return result;
        }

    }
}
