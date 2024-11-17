import React, { useState } from 'react';

export default function ImageInCard({ images, title }) {
    const [currentImageIndex, setCurrentImageIndex] = useState(0);

    const nextImage = () => {
        setCurrentImageIndex((prevIndex) =>
            prevIndex === images.length - 1 ? 0 : prevIndex + 1
        );
    };

    const prevImage = () => {
        setCurrentImageIndex((prevIndex) =>
            prevIndex === 0 ? images.length - 1 : prevIndex - 1
        );
    };

    const placeholderImage = "https://via.placeholder.com/400?text=Brak+zdjęcia";

    const currentImage = images && images.length > 0
        ? `data:${images[currentImageIndex].contentType};base64,${images[currentImageIndex].data}`
        : placeholderImage;

    return (
        <div className="md:flex-shrink-0 md:w-2/3 p-4">
            <div className="relative">
                <img
                    src={currentImage}
                    alt={`Zdjęcie książki ${title}`}
                    className="w-full h-auto object-cover rounded-lg shadow-md"
                />
                <button
                    onClick={prevImage}
                    className="absolute left-0 top-1/2 transform -translate-y-1/2 bg-black bg-opacity-50 text-white p-2 rounded-r"
                >
                    &lt;
                </button>
                <button
                    onClick={nextImage}
                    className="absolute right-0 top-1/2 transform -translate-y-1/2 bg-black bg-opacity-50 text-white p-2 rounded-l"
                >
                    &gt;
                </button>
            </div>
            <div className="flex justify-center mt-4 space-x-2">
                {images.map((_, index) => (
                    <button
                        key={index}
                        onClick={() => setCurrentImageIndex(index)}
                        className={`w-3 h-3 rounded-full ${index === currentImageIndex ? 'bg-blue-600' : 'bg-gray-300'
                            }`}
                    />
                ))}
            </div>
        </div>
    );
}