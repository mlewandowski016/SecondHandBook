import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import OfferInfo from './OfferInfo';
import ActionButtons from './ActionButtons';
import StatusBanner from './StatusBanner';
import ImageGallery from './ImageGallery';
import api from '../Api';
import { useAuth } from '../hooks/useAuth'

export default function BookDetails() {
  const [offer, setOffer] = useState(null);
  const { id } = useParams();
  const navigate = useNavigate();
  const { user } = useAuth();
  console.log('user :>> ', user);
  useEffect(() => {
    const fetchBook = async () => {
      try {
        const response = await api.get("/offers/" + id)
        console.log('response :>> ', response.data);

        const data = response.data;
        setOffer(data)

      } catch (error) {
        console.log('Error while fetching a book:', error);
      }
    }
    fetchBook();
  }, [id]);

  const handleReserve = async () => {
    const response = await api.put("/offers/" + id + "/reserve");
    console.log('response :>> ', response);
    setOffer(offer => ({
      ...offer,
      takerId: user.userId
    }));
  };

  const handleCancelReservation = async () => {
    const response = await api.put("/reservedOffers/" + id + "/unreserve");
    console.log('response :>> ', response);
    setOffer(offer => ({
      ...offer,
      takerId: 0
    }));
  };

  const handleCollect = async () => {
    const response = await api.put("/reservedOffers/" + id + "/collect");
    console.log('response :>> ', response);
    setOffer(offer => ({
      ...offer,
      isCollected: true
    }));
  };

  const handleDelete = () => {
    if (window.confirm('Are you sure you want to remove this offer??')) {
      try {
        api.delete(`/offers/${id}`);
        console.log("Offer removed successfully")
        navigate('/');
      } catch (error) {
        console.error("Error when deleting a offer from the database:", error);
        alert("Failed to delete offer.");
        return;
      }
    }
  };

  if (!offer || !user) {
    return <div className="text-center py-8">Loading...</div>;
  }

  const isCreator = user && user.id === offer.giverId;
  const isReservedByUser = user && user.id === offer.takerId;
  const isAvailable = offer.takerId === 0 && !offer.isCollected;

  return (
    <div className="min-h-screen bg-gray-100">
      <div className="container mx-auto px-4 py-8">
        <div className="bg-white shadow-lg rounded-lg overflow-hidden">
          <div className="md:flex">
            <ImageGallery images={offer.images} title={offer.title} />
            <div className="p-8 md:w-2/3">
              <OfferInfo book={offer} />
              <ActionButtons
                isCreator={isCreator}
                isReservedByUser={isReservedByUser}
                isAvailable={isAvailable}
                onReserve={handleReserve}
                onCancelReservation={handleCancelReservation}
                onCollect={handleCollect}
                onDelete={handleDelete}
                adId={offer.id}
                isCollected={offer.isCollected}
              />
            </div>
          </div>
          <StatusBanner
            isAvailable={isAvailable}
            isReservedByUser={isReservedByUser}
            isCollected={offer.isCollected}
          />
        </div>
      </div>
    </div>
  );
}