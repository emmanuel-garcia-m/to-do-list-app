import React from "react";
import "./spinner.css";
import ReactDOM from 'react-dom';

function LoadingSpinner() {
  return ReactDOM.createPortal(
    <div className="spinner-container">
      <div className="loading-spinner"></div>
    </div>,
    document.getElementById('spinner')
  );
}

export {LoadingSpinner};