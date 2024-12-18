import React from 'react';
import { useNavigate } from 'react-router-dom';

export default function ActionButtons({
  isCreator,
  isReservedByUser,
  isAvailable,
  onReserve,
  onCancelReservation,
  onCollect,
  onDelete,
  adId,
  isCollected
}) {
  const navigate = useNavigate();

  if (isCreator) {
    return (
      <div className="flex flex-wrap gap-4">
        <button
          onClick={() => navigate(`/edit-offer/${adId}`)}
          className="px-8 py-3 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition duration-300 text-lg font-semibold"
        >
          Edit offer
        </button>
        <button
          onClick={onDelete}
          className="px-8 py-3 bg-red-600 text-white rounded-lg hover:bg-red-700 transition duration-300 text-lg font-semibold"
        >
          Delete offer
        </button>
      </div>
    );
  }

  if (isAvailable) {
    return (
      <button
        onClick={onReserve}
        className="px-8 py-3 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition duration-300 text-lg font-semibold"
      >
        Reserve
      </button>
    );
  }

  if (isReservedByUser && !isCollected) {
    return (
      <div className="flex flex-wrap gap-4">
        <button
          onClick={onCollect}
          className="px-8 py-3 bg-green-500 text-white rounded-lg hover:bg-green-600 transition duration-300 text-lg font-semibold"
        >
          Collect
        </button>
        <button
          onClick={onCancelReservation}
          className="px-8 py-3 bg-yellow-500 text-white rounded-lg hover:bg-yellow-600 transition duration-300 text-lg font-semibold"
        >
          Cancel reservation
        </button>
      </div>
    );
  }

  return (
    <p className="text-gray-600 italic">Offer is not available</p>
  );
}