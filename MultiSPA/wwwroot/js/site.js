// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// To resize the iframe with content on startup and any time the content of the iframe changes, we will use a JavaScript component called iFrame Resizer created by David Bradshaw.
$('.app-container').iFrameResize({ heightCalculationMethod: 'documentElementOffset' });