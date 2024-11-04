import React from 'react';
import {Link} from "react-router-dom";
import { useAuth } from "../hooks/useAuth";

// Przykładowe dane książek
const books = [
  { id: 1, title: "Władca Pierścieni", author: "J.R.R. Tolkien", owner: "Anna Kowalska" },
  { id: 2, title: "Harry Potter", author: "J.K. Rowling", owner: "Marek Nowak" },
  { id: 3, title: "Wiedźmin", author: "Andrzej Sapkowski", owner: "Kasia Wiśniewska" },
];

  

export const HomePage = () => {
  const { logout } = useAuth();

  const handleLogout = () => {
    logout();
  };
  return (
    <div className="container mx-auto px-4">
      <header className="py-6">
        <nav className="flex justify-between items-center">
          <Link href="/" className="text-2xl font-bold">
            SecondHandBook
          </Link>
          <div className="space-x-4">
            <button onClick={handleLogout} className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700" >Logout</button>
          </div>
        </nav>
      </header>

      <main>
        <h1 className="text-3xl font-bold mb-6">Dostępne książki</h1>
        <input 
          type="search" 
          placeholder="Szukaj książek..." 
          className="w-full p-2 mb-6 border border-gray-300 rounded"
        />
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {books.map((book) => (
            <div key={book.id} className="border border-gray-200 rounded-lg p-4 shadow-sm">
              <h2 className="text-xl font-semibold mb-2">{book.title}</h2>
              <p className="text-gray-600">Autor: {book.author}</p>
              <p className="text-gray-600">Właściciel: {book.owner}</p>
              <Link 
                href={`/book/${book.id}`} 
                className="mt-4 inline-block bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
              >
                Zobacz szczegóły
              </Link>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
};
export default HomePage;