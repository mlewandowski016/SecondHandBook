import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../Api';

export default function NewOffer() {
  const [offerData, setOfferData] = useState({
    selectedBook: null,
    description: '',
    images: [],
  });

  const [searchPhrase, setSearchPhrase] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [isSearching, setIsSearching] = useState(false);
  const [files, setFiles] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    if (searchPhrase.length > 2) {
      const fetchBooks = async () => {
        try {
          const response = await api.get("/book", {
            params: {
              searchPhrase: searchPhrase
            },
          });
          console.log('response :>> ', response.data);
          setSearchResults(response.data.items);
        } catch (error) {
          console.error("Error while fetching books:", error);
        }
      };
      setIsSearching(true);
      fetchBooks();
    } else {
      setSearchResults([]);
      setIsSearching(false);
    }
  }, [searchPhrase]);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setOfferData(prevData => ({
      ...prevData,
      [name]: value
    }));
  };

  const handleImageUpload = (event) => {
    const newFiles = Array.from(event.target.files);
    
    const newImages = newFiles.map(file => URL.createObjectURL(file));
  
    setFiles(prevFiles => [...prevFiles, ...newFiles]);
  
    setOfferData(prevData => ({
      ...prevData,
      images: [...prevData.images, ...newImages],
    }));
  };

  const handleBookSelect = (book) => {
    setOfferData(prevData => ({
      ...prevData,
      selectedBook: book,
    }));
    setSearchPhrase('');
    setSearchResults([]);
    setIsSearching(false);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    const formData = new FormData();

    formData.append('BookId', offerData.selectedBook.id);
    formData.append('OfferDescription', offerData.description);
    Array.from(files).forEach((file) => {
      formData.append('Images', file);
    });

    try {
      const response = await api.post("/offers/", formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });
      console.log('response :>> ', response);
      alert('The offer was created!');
      navigate('/');
    } catch (error) {
      console.error("Error when creating an offer:", error);
    }
  };

  return (
    <div className="min-h-screen bg-gray-100 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-3xl mx-auto bg-white rounded-lg shadow-md p-6">
        <h1 className="text-3xl font-bold text-center mb-8">Create a new offer</h1>
        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label htmlFor="book-search" className="block text-sm font-medium text-gray-700 mb-1">
              Select a book
            </label>
            <div className="relative">
              <input
                type="text"
                id="book-search"
                className="w-full p-2 border border-gray-300 rounded-md"
                placeholder="Search..."
                value={searchPhrase}
                onChange={(e) => setSearchPhrase(e.target.value)}
              />
              {isSearching && (
                <ul className="absolute z-10 w-full bg-white border border-gray-300 rounded-md mt-1 max-h-60 overflow-auto">
                  {searchResults.map((book) => (
                    <li
                      key={book.id}
                      className="p-2 hover:bg-gray-100 cursor-pointer"
                      onClick={() => handleBookSelect(book)}
                    >
                      {book.title} - {book.author} - ISBN {book.isbn}
                    </li>
                  ))}
                </ul>
              )}
            </div>
            {offerData.selectedBook && (
              <p className="mt-2 text-sm text-gray-600">
                Selected book: {offerData.selectedBook.title} - {offerData.selectedBook.author} - ISBN {offerData.selectedBook.isbn}
              </p>
            )}
          </div>
          <div>
            <label htmlFor="description" className="block text-sm font-medium text-gray-700 mb-1">
              Description
            </label>
            <textarea
              id="description"
              name="description"
              rows="4"
              className="w-full p-2 border border-gray-300 rounded-md"
              placeholder="Describe the condition of the book, additional information...."
              value={offerData.description}
              onChange={handleChange}
            ></textarea>
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Add photos
            </label>
            <input
              type="file"
              accept="image/*"
              multiple
              onChange={handleImageUpload}
              className="w-full text-sm text-gray-500
                file:mr-4 file:py-2 file:px-4
                file:rounded-full file:border-0
                file:text-sm file:font-semibold
                file:bg-blue-50 file:text-blue-700
                hover:file:bg-blue-100"
            />
          </div>
          {offerData.images.length > 0 && (
            <div className="grid grid-cols-3 gap-4 mt-4">
              {offerData.images.map((image, index) => (
                <img key={index} src={image} alt={`Uploaded preview ${index + 1}`} className="w-full h-32 object-cover rounded-md" />
              ))}
            </div>
          )}
          <div>
            <button
              type="submit"
              className="w-full bg-blue-600 text-white p-2 rounded-md hover:bg-blue-700 transition duration-300"
              disabled={!offerData.selectedBook}
            >
              Publish
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
