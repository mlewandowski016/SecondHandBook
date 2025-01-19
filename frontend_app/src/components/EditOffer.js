import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import api from '../Api';

export default function EditOffer() {
  const { id } = useParams();
  const [offerData, setOfferData] = useState({
    offerDescription: '',
    images: [],
  });

  const [files, setFiles] = useState([]);
  const [descriptionError, setDescriptionError] = useState('');
  const [removeImageError, setRemoveImageError] = useState('');
  const [imageError, setImageError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchOfferData = async () => {
      try {
        const response = await api.get(`/offers/${id}`);
        const { offerDescription, images } = response.data;

        const convertedImages = images.map((image) => ({
          id: image.id,
          src: `data:${image.contentType};base64,${image.data}`,
          isFromAPI: true,
        }));

        setOfferData({
          offerDescription: offerDescription,
          images: convertedImages,
        });
        console.log('response.data :>> ', response.data);
      } catch (error) {
        console.error("Error while fetching offer:", error);
        alert("Failed to load offer's data.");
        navigate('/');
      }
    };

    fetchOfferData();
  }, [id, navigate]);

  const handleDescriptionChange = (event) => {
    const newDescription = event.target.value;
    setOfferData((prevData) => ({
      ...prevData,
      offerDescription: newDescription,
    }));
    setDescriptionError('');
  };

  const handleImageUpload = (event) => {
    const uploadedFiles = Array.from(event.target.files);
    setFiles((prevFiles) => [...prevFiles, ...uploadedFiles]);

    const newImageURLs = uploadedFiles.map((file) => ({
      id: null,
      src: URL.createObjectURL(file),
      isFromAPI: false,
      file,
    }));

    setOfferData((prevData) => ({
      ...prevData,
      images: [...prevData.images, ...newImageURLs],
    }));

    setImageError('');
  };

  const handleRemoveImage = async (image, index) => {
    if (offerData.images.length === 1) {
      setRemoveImageError('The offer must include at least one photo.');
      return;
    }

    if (image.isFromAPI) {
      try {
        await api.delete(`/offers/image/${image.id}`);
        console.log('Photo removed from database.');
      } catch (error) {
        console.error("Error when deleting a photo from the database:", error);
        alert("Failed to delete photo.");
        return;
      }
    } else {
      setFiles((prevFiles) => prevFiles.filter((_, i) => i !== index));
    }

    setOfferData((prevData) => ({
      ...prevData,
      images: prevData.images.filter((_, i) => i !== index),
    }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!offerData.offerDescription || !offerData.offerDescription.trim()) {
      setDescriptionError('The description must not be empty.');
      return;
    }

    if (offerData.images.length === 0) {
      setImageError('The offer must include at least one photo.');
      return;
    }

    const formData = new FormData();
    formData.append('OfferDescription', offerData.offerDescription);

    Array.from(files).forEach((file) => {
      formData.append('Images', file);
    });

    try {
      await api.put(`/offers/edit/${id}`, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      alert('The offer has been updated!');
      navigate('/');
    } catch (error) {
      console.error("Error while updating the offer:", error);
      alert('Failed to update offer.');
    }
  };

  return (
    <div className="max-w-2xl mx-auto bg-white p-6 shadow-md rounded-lg">
      <h2 className="text-xl font-bold mb-4">Edit offer</h2>
      <form onSubmit={handleSubmit} className="space-y-6">
        <div>
          <label htmlFor="description" className="block text-sm font-medium text-gray-700 mb-1">
            Description
          </label>
          <textarea
            id="description"
            name="description"
            rows="4"
            className="w-full p-2 border border-gray-300 rounded-md"
            placeholder="Update your offer description"
            value={offerData.offerDescription}
            onChange={handleDescriptionChange}
          ></textarea>
          {descriptionError && (
            <p className="text-red-500 text-sm mt-2">{descriptionError}</p>
          )}
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Add new images
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
          {imageError && (
            <p className="text-red-500 text-sm mt-2">{imageError}</p>
          )}
        </div>
        {offerData.images.length > 0 && (
          <div className="grid grid-cols-3 gap-4 mt-4">
            {offerData.images.map((image, index) => (
              <div key={index} className="relative">
                <img
                  src={image.src}
                  alt={`Obraz ${index + 1}`}
                  className="w-full h-32 object-cover rounded-md"
                />
                <button
                  type="button"
                  className="absolute top-0 right-0 bg-red-500 text-white rounded-full p-1"
                  onClick={() => handleRemoveImage(image, index)}
                >
                  &times;
                </button>
              </div>
            ))}
            {removeImageError && (
              <p className="text-red-500 text-sm mt-2">{removeImageError}</p>
            )}
          </div>
        )}
        <div className="flex justify-end">
          <button
            type="submit"
            className="w-full bg-blue-600 text-white p-2 rounded-md hover:bg-blue-700 transition duration-300"
          >
            Update offer
          </button>
        </div>
      </form>
    </div>
  );
}
