using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace libraryBooksManagement
{
    public partial class MainForm : Form
    {
        private NpgsqlConnection conn;
        private string connString = "Host=localhost;Username=postgres;Password=root;Database=testDB";

        // Constructor
        public MainForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        // Auto-generated method to initialize components
        private void InitializeComponent()
        {
            this.dataGridViewBooks = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBooks
            // 
            this.dataGridViewBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBooks.Location = new System.Drawing.Point(9, 10);
            this.dataGridViewBooks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewBooks.Name = "dataGridViewBooks";
            this.dataGridViewBooks.Size = new System.Drawing.Size(826, 346);
            this.dataGridViewBooks.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 366);
            this.Controls.Add(this.dataGridViewBooks);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "Library Books Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).EndInit();
            this.ResumeLayout(false);
        }

        // Method to initialize DataGridView with column headers
        private void InitializeDataGridView()
        {
            dataGridViewBooks.ColumnCount = 6;
            dataGridViewBooks.Columns[0].Name = "Title";
            dataGridViewBooks.Columns[1].Name = "Description";
            dataGridViewBooks.Columns[2].Name = "Genre";
            dataGridViewBooks.Columns[3].Name = "Shelf";
            dataGridViewBooks.Columns[4].Name = "ISBN";
            dataGridViewBooks.Columns[5].Name = "Available to Lend";
        }

        // Method called when MainForm is loaded
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Establish connection to PostgreSQL
            conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open(); // Open the connection
                CreateTable(); // Create the 'books' table if not exists
                InsertSampleRecords(); // Insert sample records into the 'books' table
                ReadRecords(); // Read records from 'books' table and display in DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Method to create the 'books' table in the database
        private void CreateTable()
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS books (
                    title TEXT,
                    description TEXT,
                    genre TEXT,
                    shelf TEXT,
                    isbn TEXT,
                    available_to_lend BOOLEAN
                );";

            using (var cmd = new NpgsqlCommand(createTableQuery, conn))
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Table 'books' created successfully.");
            }
        }

        // Method to insert sample records into the 'books' table
        private void InsertSampleRecords()
        {
            string[] titles = {
                "The Great Gatsby",
                "To Kill a Mockingbird",
                "Pride and Prejudice",
                "1984",
                "The Catcher in the Rye"
            };

            string[] descriptions = {
                "Classic novel about the American Dream.",
                "Exploration of racial injustice and moral growth.",
                "Romantic novel by Jane Austen.",
                "Dystopian novel by George Orwell.",
                "Coming-of-age novel by J.D. Salinger."
            };

            string[] genres = {
                "Fiction",
                "Fiction",
                "Romance",
                "Dystopian",
                "Coming-of-age"
            };

            string[] shelves = {
                "A1",
                "B2",
                "C3",
                "D4",
                "E5"
            };

            string[] isbns = {
                "9780743273565",
                "9780061120084",
                "9781613821851",
                "9780451524935",
                "9780316769488"
            };

            bool[] availableToLend = {
                true,
                false,
                true,
                true,
                false
            };

            for (int i = 0; i < titles.Length; i++)
            {
                string insertQuery = @"
                    INSERT INTO books (title, description, genre, shelf, isbn, available_to_lend)
                    VALUES (@Title, @Description, @Genre, @Shelf, @ISBN, @AvailableToLend);";

                using (var cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", titles[i]);
                    cmd.Parameters.AddWithValue("@Description", descriptions[i]);
                    cmd.Parameters.AddWithValue("@Genre", genres[i]);
                    cmd.Parameters.AddWithValue("@Shelf", shelves[i]);
                    cmd.Parameters.AddWithValue("@ISBN", isbns[i]);
                    cmd.Parameters.AddWithValue("@AvailableToLend", availableToLend[i]);

                    cmd.ExecuteNonQuery(); // Execute the SQL command
                }
            }

            MessageBox.Show("Sample records inserted into 'books' table.");
        }

        // Method to read records from 'books' table and display in DataGridView
        private void ReadRecords()
        {
            dataGridViewBooks.Rows.Clear(); // Clear existing rows

            string selectQuery = "SELECT title, description, genre, shelf, isbn, available_to_lend FROM books;";

            using (var cmd = new NpgsqlCommand(selectQuery, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataGridViewBooks.Rows.Add(
                            reader["title"],
                            reader["description"],
                            reader["genre"],
                            reader["shelf"],
                            reader["isbn"],
                            reader["available_to_lend"]
                        );
                    }
                }
            }

            MessageBox.Show("Records retrieved and displayed in DataGridView.");
        }

        // Method called when MainForm is closing
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close(); // Close the connection
                conn.Dispose(); // Dispose of the connection object
            }
        }
    }
}
