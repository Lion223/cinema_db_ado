using System;
using System.Data;

namespace CinemaApp
{
    class Program
    {
        // show db tables
        static public void viewDb(DataTable films, DataTable genre, DataTable actors, DataTable fga)
        {
            Console.WriteLine("\nShowing DB...\n");
            Console.WriteLine(films.TableName + " ");
            foreach (DataRow x in films.Rows)
            {
                foreach (DataColumn y in films.Columns)
                {
                    Console.Write(x[y] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n" + genre.TableName + " ");
            foreach (DataRow x in genre.Rows)
            {
                foreach (DataColumn y in genre.Columns)
                {
                    Console.Write(x[y] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n" + actors.TableName + " ");
            foreach (DataRow x in actors.Rows)
            {
                foreach (DataColumn y in actors.Columns)
                {
                    Console.Write(x[y] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n" + fga.TableName + " ");
            foreach (DataRow x in fga.Rows)
            {
                foreach (DataColumn y in fga.Columns)
                {
                    Console.Write(x[y] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            // create table Films
            DataTable films = new DataTable("Films");
            DataColumn id = new DataColumn("Id");
            id.DataType = typeof(int);
            id.Unique = true;
            id.AllowDBNull = false;
            id.Caption = "Id";
            films.Columns.Add(id);

            DataColumn name = new DataColumn("Name");
            name.DataType = typeof(string);
            name.MaxLength = 25;
            name.Unique = false;
            name.AllowDBNull = false;
            name.Caption = "Name";
            films.Columns.Add(name);

            DataColumn year = new DataColumn("Year");
            year.DataType = typeof(int);
            year.Unique = false;
            year.AllowDBNull = true;
            year.Caption = "Year";
            films.Columns.Add(year);

            films.PrimaryKey = new DataColumn[] { id };

            // create table Genre
            DataTable genre = new DataTable("Genre");
            DataColumn id_g = new DataColumn("Id");
            id_g.DataType = typeof(int);
            id_g.Unique = true;
            id_g.AllowDBNull = false;
            id_g.Caption = "Id";
            genre.Columns.Add(id_g);

            DataColumn name_g = new DataColumn("Name");
            name_g.DataType = typeof(string);
            name_g.MaxLength = 25;
            name_g.Unique = false;
            name_g.AllowDBNull = false;
            name_g.Caption = "Name";
            genre.Columns.Add(name_g);

            genre.PrimaryKey = new DataColumn[] { id_g };

            // create table Actors
            DataTable actors = new DataTable("Actors");
            DataColumn id_a = new DataColumn("Id");
            id_a.DataType = typeof(int);
            id_a.Unique = true;
            id_a.AllowDBNull = false;
            id_a.Caption = "Id";
            actors.Columns.Add(id_a);

            DataColumn firstName = new DataColumn("firstName");
            firstName.DataType = typeof(string);
            firstName.MaxLength = 25;
            firstName.Unique = false;
            firstName.AllowDBNull = false;
            firstName.Caption = "firstName";
            actors.Columns.Add(firstName);

            DataColumn lastName = new DataColumn("lastName");
            lastName.DataType = typeof(string);
            lastName.MaxLength = 25;
            lastName.Unique = false;
            lastName.AllowDBNull = false;
            lastName.Caption = "lastName";
            actors.Columns.Add(lastName);

            actors.PrimaryKey = new DataColumn[] { id_a };

            // create intermediate table FGA
            DataTable fga = new DataTable("FGA");
            DataColumn id_f = new DataColumn("Film_Id");
            id_f.DataType = typeof(int);
            id_f.Unique = true;
            id_f.AllowDBNull = false;
            id_f.Caption = "Film_Id";
            fga.Columns.Add(id_f);

            DataColumn id_fg = new DataColumn("Genre_Id");
            id_fg.DataType = typeof(int);
            id_fg.Unique = true;
            id_fg.AllowDBNull = false;
            id_fg.Caption = "Genre_Id";
            fga.Columns.Add(id_fg);

            DataColumn id_fga = new DataColumn("Actor_Id");
            id_fga.DataType = typeof(int);
            id_fga.Unique = true;
            id_fga.AllowDBNull = false;
            id_fga.Caption = "Actor_Id";
            fga.Columns.Add(id_fga);

            fga.PrimaryKey = new DataColumn[] { id_f, id_fg, id_fga };

            // create DataSet
            DataSet cinema = new DataSet("Cinema");
            cinema.Tables.Add(films);
            cinema.Tables.Add(genre);
            cinema.Tables.Add(actors);
            cinema.Tables.Add(fga);

            cinema.Relations.Add("films_throughttable", films.Columns["Id"], fga.Columns["Film_Id"]);
            cinema.Relations.Add("genre_throughttable", genre.Columns["Id"], fga.Columns["Genre_Id"]);
            cinema.Relations.Add("actors_throughttable", actors.Columns["Id"], fga.Columns["Actor_Id"]);

            // enter information
            films.Rows.Add(1, "Warcraft", 2016);
            films.Rows.Add(2, "Blade Runner 2049", 2017);
            films.Rows.Add(3, "Badass", 2017);
            films.Rows.Add(4, "The Predator", 2018);
            films.Rows.Add(5, "Star Wars. Episode IX", 2019);

            genre.Rows.Add(1, "Fantasy");
            genre.Rows.Add(2, "Cyberpunk");
            genre.Rows.Add(3, "Action");
            genre.Rows.Add(4, "Horror");
            genre.Rows.Add(5, "Fantasy");

            actors.Rows.Add(1, "Travis", "Fimmel");
            actors.Rows.Add(2, "Ryan", "Gosling");
            actors.Rows.Add(3, "Daisy", "Ridley");
            actors.Rows.Add(4, "Boyd", "Holbrook");
            actors.Rows.Add(5, "Mark", "Hamill");

            fga.Rows.Add(1, 1, 1);
            fga.Rows.Add(2, 2, 2);
            fga.Rows.Add(3, 3, 3);
            fga.Rows.Add(4, 4, 4);
            fga.Rows.Add(5, 5, 5);

            cinema.AcceptChanges();

            viewDb(films, genre, actors, fga);

            // create DataView that sorts film names by ascending
            Console.WriteLine("\nDataView - \"Films\", name by ascending");
            DataView view = new DataView(films);
            view.Sort = "Name ASC";

            foreach (DataRowView dr in view)
            {
                foreach (DataColumn dc in view.Table.Columns)
                {
                    Console.Write(dr.Row[dc] + " ");
                }
                Console.WriteLine();
            }

            // create DataView that filters upcoming films
            Console.WriteLine("\nDataView - \"Films\", after 2017");
            DataView view_1 = new DataView(films);
            view_1.RowFilter = "Year > 2017";

            foreach (DataRowView dr in view_1)
            {
                foreach (DataColumn dc in view_1.Table.Columns)
                {
                    Console.Write(dr.Row[dc] + " ");
                }
                Console.WriteLine();
            }

            // modify info
            films.LoadDataRow(new object[] { 1, "Changed", 1997 }, false);

            // add info
            films.LoadDataRow(new object[] { 6, "Chicken", 2005 }, false);
            genre.Rows.Add(6, "Comedy");
            actors.Rows.Add(6, "Leeroy", "Jenkins");
            fga.Rows.Add(6, 6, 6);

            // delete info
            films.Rows[1].Delete(); //2-nd row in table numeration
            genre.Rows[1].Delete();
            actors.Rows[1].Delete();
            fga.Rows[1].Delete();

            // create DataSet that shows results of data changing
            DataSet cinema_1 = cinema.GetChanges();
            Console.WriteLine("\nChanged data");
            foreach (DataTable tables in cinema_1.Tables)
            {
                Console.WriteLine(tables.TableName);
                foreach (DataRow dr in tables.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        foreach (DataColumn dc_d in tables.Columns)
                        {
                            Console.Write(tables.Rows[0][dc_d, DataRowVersion.Original] + " ");
                        }

                    }
                    else
                    {
                        foreach (DataColumn dc in tables.Columns)
                        {
                            Console.Write(dr[dc] + " ");
                        }
                    }

                    Console.Write(dr.RowState);
                    Console.WriteLine();
                }
            }

            cinema.AcceptChanges();

            // final version of db
            viewDb(films, genre, actors, fga);
            Console.ReadKey();
        }
    }
}
