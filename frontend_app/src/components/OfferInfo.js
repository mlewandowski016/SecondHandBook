import React from 'react';

export default function OfferInfo({ book: offer }) {
  return (
    <>
      <h1 className="text-4xl font-bold mb-4">{offer.title}</h1>
      <p className="text-2xl text-gray-600 mb-6">{offer.author}</p>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-8">
        <p><span className="font-semibold">ISBN:</span> {offer.isbn}</p>
        <p><span className="font-semibold">Data wydania:</span> {offer.publishDate}</p>
        <p><span className="font-semibold">Liczba stron:</span> {offer.pagesCount}</p>
        <p><span className="font-semibold">Właściciel:</span> {offer.name} {offer.lastname}</p>
      </div>
      <h3 className="text-2xl font-semibold mb-4">Opis</h3>
      <p className="text-gray-700 mb-8 text-lg leading-relaxed">{offer.bookDescription}</p>
      <h3 className="text-2xl font-semibold mb-4">Słowa właściciela</h3>
      <p className="text-gray-700 mb-8 text-lg leading-relaxed">{offer.offerDescription}</p>
    </>
  );
}