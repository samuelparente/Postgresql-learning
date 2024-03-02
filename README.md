# Library Books Management

This Windows Forms application is designed to manage library books using a PostgreSQL database. It allows users to view book records.

## Features

- View a list of library books with details such as title, description, genre, shelf, ISBN, and availability.

## Technologies Used

- C# for the application logic.
- PostgreSQL for the database.
- Windows Forms for the user interface.

## Sample Data

The application comes with sample data pre-inserted into the `books` table. Here are some of the sample records:

| Title               | Description                            | Genre         | Shelf | ISBN         | Available to Lend |
|---------------------|----------------------------------------|---------------|-------|--------------|-------------------|
| The Great Gatsby    | Classic novel about the American Dream | Fiction       | A1    | 9780743273565| Yes               |
| To Kill a Mockingbird| Exploration of racial injustice        | Fiction       | B2    | 9780061120084| No                |
| Pride and Prejudice | Romantic novel by Jane Austen          | Romance       | C3    | 9781613821851| Yes               |
| 1984                | Dystopian novel by George Orwell       | Dystopian     | D4    | 9780451524935| Yes               |
| The Catcher in the Rye| Coming-of-age novel by J.D. Salinger | Coming-of-age| E5    | 9780316769488| No                |

## Notes

- This application is for demonstration of some basic concepts-
- Ensure that your PostgreSQL connection string in `MainForm.cs` matches your local setup.
- The application allows basic CRUD operations on the `books` table.
- Make sure to dispose of the PostgreSQL connection properly to avoid resource leaks.
