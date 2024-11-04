import React, { useState } from 'react';
import {Link} from "react-router-dom";

// Przykładowe dane książki (w rzeczywistej aplikacji pobierałbyś te dane na podstawie ID)
const book = {
  id: 1,
  title: "Władca Pierścieni",
  author: "J.R.R. Tolkien",
  owner: "Anna Kowalska",
  isbn: "978-0618640157",
  publishDate: "1954-07-29",
  description: "Epicka opowieść o hobbitach i ich misji zniszczenia Jedynego Pierścienia.",
  pageCount: 1178,
};

export default function BookDetails() {
  const [isReserved, setIsReserved] = useState(false);

  const handleReservation = () => {
    // Tutaj dodałbyś logikę rezerwacji
    setIsReserved(true);
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <Link href="/" className="text-blue-600 hover:text-blue-800 mb-6 inline-block">
        &larr; Powrót do listy książek
      </Link>
      <div className="bg-white shadow-md rounded-lg p-6">
        <h1 className="text-3xl font-bold mb-2">{book.title}</h1>
        <p className="text-xl text-gray-600 mb-6">{book.author}</p>
        <div className="flex flex-col md:flex-row md:space-x-6">
          <div className="w-full md:w-1/3 mb-6 md:mb-0">
            <img
              src={`https://via.placeholder.com/300x450?text=${encodeURIComponent(book.title)}`}
              alt={`Okładka książki ${book.title}`}
              className="w-full h-auto rounded-lg shadow-md"
            />
          </div>
          <div className="w-full md:w-2/3">
            <p className="mb-2"><span className="font-semibold">ISBN:</span> {book.isbn}</p>
            <p className="mb-2"><span className="font-semibold">Data wydania:</span> {book.publishDate}</p>
            <p className="mb-2"><span className="font-semibold">Liczba stron:</span> {book.pageCount}</p>
            <p className="mb-2"><span className="font-semibold">Właściciel:</span> {book.owner}</p>
            <h3 className="text-xl font-semibold mt-6 mb-2">Opis</h3>
            <p className="text-gray-700">{book.description}</p>
          </div>
        </div>
        <button 
          onClick={handleReservation}
          disabled={isReserved}
          className={`mt-6 px-6 py-2 rounded ${
            isReserved 
              ? 'bg-gray-400 cursor-not-allowed' 
              : 'bg-blue-600 hover:bg-blue-700 text-white'
          }`}
        >
          {isReserved ? 'Zarezerwowano' : 'Zarezerwuj'}
        </button>
      </div>
    </div>
  );
}