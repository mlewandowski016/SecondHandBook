import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { useState } from "react";
import api from '../Api';
import ImageInCard from "./ImageInCard";

export const HomePage = () => {
  const [searchPhrase, setSearchPhrase] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(12);
  const [books, setBooks] = useState([]);
  const [totalPages, setTotalPages] = useState(1);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const response = await api.get("/offers", {
          params: {
            searchPhrase: searchPhrase,
            pageNumber: pageNumber,
            pageSize: pageSize,
          },
        });

        console.log('response.data :>> ', response.data);
        const data = response.data;
        setBooks(data.items);
        setTotalPages(data.totalPages);
      } catch (error) {
        console.error("Błąd podczas pobierania książek:", error);
      }
    };

    fetchBooks();
  }, [searchPhrase, pageNumber, pageSize]);



  const handleSearchChange = (event) => {
    setSearchPhrase(event.target.value);
    setPageNumber(1); // Resetuj numer strony po zmianie frazy wyszukiwania
  };

  const handleNextPage = () => {
    if (pageNumber < totalPages) setPageNumber(pageNumber + 1);
  };

  const handlePreviousPage = () => {
    if (pageNumber > 1) setPageNumber(pageNumber - 1);
  };

  return (
    <div className="container mx-auto px-4">
      <main>
        <p className="text-3xl font-bold mb-6">Available offers</p>

        <input
          type="search"
          placeholder="Search..."
          value={searchPhrase}
          onChange={handleSearchChange}
          className="w-full p-2 mb-6 border border-gray-300 rounded"
        />

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {books.map((book) => (
            <div key={book.id} className="bg-white border border-gray-200 rounded-lg overflow-hidden shadow-md hover:shadow-lg transition-shadow duration-300">
              <div className="flex flex-col sm:flex-row h-full">
                <div className="sm:w-1/2 relative">
                  <div className="aspect-w-3 aspect-h-4 sm:h-full">
                    <ImageInCard images={book.images} title={book.title} />
                  </div>
                </div>
                <div className="sm:w-1/2 p-4 flex flex-col justify-between">
                  <div>
                    <h2 className="text-lg font-semibold mb-2 text-gray-800 line-clamp-2">{book.title}</h2>
                    <p className="text-sm text-gray-600 mb-1">
                      <span className="font-medium">Author:</span> {book.author}
                    </p>
                    <p className="text-sm text-gray-600 mb-3">
                      <span className="font-medium">Owner:</span> {book.name} {book.lastname}
                    </p>
                  </div>
                  <Link
                    to={`/offer/${book.id}`}
                    className="inline-block w-full text-center bg-blue-600 text-white px-3 py-2 rounded text-sm hover:bg-blue-700 transition-colors duration-300"
                  >
                    See more
                  </Link>
                </div>
              </div>
            </div>
          ))}
        </div>

        <div className="flex justify-between items-center mt-6">
          <button
            onClick={handlePreviousPage}
            disabled={pageNumber === 1}
            className="bg-gray-200 text-gray-700 px-4 py-2 rounded disabled:opacity-50"
          >
            Poprzednia
          </button>
          <span>Strona {pageNumber} z {totalPages}</span>
          <button
            onClick={handleNextPage}
            disabled={pageNumber === totalPages}
            className="bg-gray-200 text-gray-700 px-4 py-2 rounded disabled:opacity-50"
          >
            Następna
          </button>
        </div>
      </main>
    </div>
  );
};

export default HomePage;