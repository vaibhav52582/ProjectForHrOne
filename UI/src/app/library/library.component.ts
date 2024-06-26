import { Component, OnInit } from '@angular/core';
import { Book, CategoryBooks } from '../models/models';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss'],
})
export class LibraryComponent implements OnInit {
  availableBooks: Book[] = [];
  booksToDisplay: CategoryBooks[] = [];
  displayedColumns: string[] = [
    'id',
    'title',
    'author',
    'price',
    'available',
  ];

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.api.getAllBooks().subscribe({
    next:(res: Book[]) => {
        this.availableBooks = [];
        console.log(res);
        for (var book of res) this.availableBooks.push(book);
        this.updateList();
      },
      error: (err: any) => console.log(err),
    });
  }

  updateList() {
    this.booksToDisplay = [];
    for (let book of this.availableBooks) {
      let exist = false;
      for (let categoryBooks of this.booksToDisplay) {
        if (
          book.category === categoryBooks.category &&
          book.subCategory === categoryBooks.subCategory
        )
          exist = true;
      }

      if (exist) {
        for (let categoryBooks of this.booksToDisplay) {
          if (
            book.category === categoryBooks.category &&
            book.subCategory === categoryBooks.subCategory
          )
            categoryBooks.books.push(book);
        }
      } else {
        this.booksToDisplay.push({
          category: book.category,
          subCategory: book.subCategory,
          books: [book],
        });
      }
    }
  }

  getBookCount() {
    return this.booksToDisplay.reduce((pv, cv) => cv.books.length + pv, 0);
  }

  search(value: string) {
    value = value.toLowerCase();
    this.updateList();
    if (value.length > 0) {
      this.booksToDisplay = this.booksToDisplay.filter((categoryBooks) => {
        categoryBooks.books = categoryBooks.books.filter(
          (book) =>
            book.title.toLowerCase().includes(value) ||
            book.author.toLowerCase().includes(value)
        );
        return categoryBooks.books.length > 0;
      });
    }
  }


  isBlocked() {
    let blocked = this.api.getTokenUserInfo()?.blocked ?? true;
    return blocked;
  }
}
