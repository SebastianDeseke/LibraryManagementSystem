using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Models;

public class Shelf {
    //A shelve is used to orden books under the same cateegory (Author or Genre?)
    public int ShelveId { get; set; }
    public string ShelveName { get; set; }
    public ICollection<Book> Books { get; set; }
    public string SharingAttribute { get; set; }
    public Section Section { get; set; }

    public Shelf (string ShelveName, string SharingAttribute, Section Section) {
        this.ShelveName = ShelveName;
        this.SharingAttribute = SharingAttribute;
        this.Section = Section;
    }

    public void AddBook(Book book) {
        Books.Add(book);
    }

    public void RemoveBook(Book book) {
        Books.Remove(book);
    }

    public void makeShelfID(){
        //Method to make a shelve ID
    }
}