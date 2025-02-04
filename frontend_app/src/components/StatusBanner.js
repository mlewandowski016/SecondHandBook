import React from 'react';

export default function StatusBanner({ isAvailable, isReservedByUser, isCollected }) {
  if (isCollected) {
    return (
      <div className="bg-gray-100 border-t-4 border-gray-500 rounded-b text-gray-900 px-4 py-3 shadow-md" role="alert">
        <div className="flex">
          <div className="py-1">
            <svg className="fill-current h-6 w-6 text-gray-500 mr-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
              <path d="M2.93 17.07A10 10 0 1 1 17.07 2.93 10 10 0 0 1 2.93 17.07zm12.73-1.41A8 8 0 1 0 4.34 4.34a8 8 0 0 0 11.32 11.32zM9 11V9h2v6H9v-4zm0-6h2v2H9V5z"/>
            </svg>
          </div>
          <div>
            <p className="font-bold">Offer completed</p>
            <p className="text-sm">This book has already been received and is not available.</p>
          </div>
        </div>
      </div>
    );
  }

  if (!isAvailable && !isReservedByUser) {
    return (
      <div className="bg-yellow-100 border-t-4 border-yellow-500 rounded-b text-yellow-900 px-4 py-3 shadow-md" role="alert">
        <div className="flex">
          <div className="py-1">
            <svg className="fill-current h-6 w-6 text-yellow-500 mr-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
              <path d="M2.93 17.07A10 10 0 1 1 17.07 2.93 10 10 0 0 1 2.93 17.07zm12.73-1.41A8 8 0 1 0 4.34 4.34a8 8 0 0 0 11.32 11.32zM9 11V9h2v6H9v-4zm0-6h2v2H9V5z"/>
            </svg>
          </div>
          <div>
            <p className="font-bold">Book reserved</p>
            <p className="text-sm">This book is currently reserved by another user.</p>
          </div>
        </div>
      </div>
    );
  }

  return null;
}