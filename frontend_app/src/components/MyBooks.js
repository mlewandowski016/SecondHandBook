import React, { useEffect } from "react";
import { useState } from "react";
import api from '../Api';

export const MyBooks = () => {
    const [books, setBooks] = useState([]);

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const response = await api.get("myBooks");

                console.log('response :>> ', response);
                const data = response.data;
                setBooks(data);
            } catch (error) {
                console.error("Błąd podczas pobierania książek:", error);
            }
        };

        fetchBooks();
    }, []);

    const formatDate = (dateString) => {
        return new Date(dateString).toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    };

    if (!books) {
        return ("");
    }

    return (
        <div className="container mx-auto px-4 py-8">
            <h1 className="text-3xl font-bold mb-6">My Collected Books</h1>
            <div className="bg-white shadow-md rounded-lg overflow-hidden">
                <ul className="divide-y divide-gray-200">
                    {books.map((book) => (
                        <li key={book.id} className="p-6 hover:bg-gray-50 transition-colors duration-150 ease-in-out">
                            <div className="flex flex-col md:flex-row md:justify-between md:items-center">
                                <div className="flex-grow">
                                    <h2 className="text-xl font-semibold text-gray-800 mb-2">{book.title}</h2>
                                    <p className="text-gray-600 mb-2">By {book.author}</p>
                                    <p className="text-sm text-gray-500 mb-1">Category: {book.category}</p>
                                    <p className="text-sm text-gray-500 mb-1">Pages: {book.pagesCount}</p>
                                    <p className="text-sm text-gray-500 mb-1">Published: {formatDate(book.publishDate)}</p>
                                    <p className="text-sm text-gray-500 mb-1">ISBN: {book.isbn}</p>
                                    <p className="text-sm text-gray-500">Added to collection: {formatDate(book.addedDate)}</p>
                                </div>
                            </div>
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    );
};

export default MyBooks;