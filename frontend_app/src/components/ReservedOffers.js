import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import api from '../Api';
import ImageInCard from "./ImageInCard";

export default function ReservedOffers() {
    const [books, setBooks] = useState([]);

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const response = await api.get("reservedOffers")

                console.log('response :>> ', response);
                const data = response.data;
                setBooks(data);
            } catch (error) {
                console.error("Błąd podczas pobierania książek:", error);
            }
        };

        fetchBooks();
    }, []);

    if (!books) {
        return ("");
    }

    return (
        <div className="container mx-auto px-4">
            <main>
                <p className="text-3xl font-bold mb-6">Reserved offers</p>

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

            </main>
        </div>
    );
};