import React from 'react';
import { Link } from "react-router-dom";

const Footer = () => {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="bg-gray-800 text-white py-8">
      <div className="container mx-auto px-4">
        <div className="flex flex-wrap justify-between">
          <div className="w-full md:w-1/3 mb-6 md:mb-0">
            <h3 className="text-xl font-bold mb-4">SecondHandBook</h3>
            <p className="text-gray-400">Share and discover books with book lovers around you.</p>
          </div>
          <div className="w-full md:w-1/3 mb-6 md:mb-0">
            <h4 className="text-lg font-semibold mb-4">Quick Links</h4>
            <ul className="space-y-2">
              <li><Link href="/" className="hover:text-blue-400 transition-colors duration-300">Home</Link></li>
              <li><Link href="/about" className="hover:text-blue-400 transition-colors duration-300">About Us</Link></li>
              <li><Link href="/contact" className="hover:text-blue-400 transition-colors duration-300">Contact</Link></li>
              <li><Link href="/terms" className="hover:text-blue-400 transition-colors duration-300">Terms of Service</Link></li>
              <li><Link href="/privacy" className="hover:text-blue-400 transition-colors duration-300">Privacy Policy</Link></li>
            </ul>
          </div>
          <div className="w-full md:w-1/3">
            <h4 className="text-lg font-semibold mb-4">Connect With Us</h4>
            <ul className="space-y-2">
              <li><a href="https://facebook.com" target="_blank" rel="noopener noreferrer" className="hover:text-blue-400 transition-colors duration-300">Facebook</a></li>
              <li><a href="https://twitter.com" target="_blank" rel="noopener noreferrer" className="hover:text-blue-400 transition-colors duration-300">Twitter</a></li>
              <li><a href="https://instagram.com" target="_blank" rel="noopener noreferrer" className="hover:text-blue-400 transition-colors duration-300">Instagram</a></li>
              <li><a href="https://github.com" target="_blank" rel="noopener noreferrer" className="hover:text-blue-400 transition-colors duration-300">GitHub</a></li>
            </ul>
          </div>
        </div>
        <div className="mt-8 pt-8 border-t border-gray-700 text-center text-gray-400">
          <p>&copy; {currentYear} SecondHandBook. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;