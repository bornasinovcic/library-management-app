/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";

let _randomNumber = "";
let _bookTitle = "";
let _bookDescription = "";
let _bookPublishDate = "";
let _bookGenre = "";
let _bookAuthors = [];

function getRandomDate() {
    let start = new Date();
    start.setFullYear(start.getFullYear() - 100); // Set start date to 100 years ago
    let end = new Date(); // End date (current date)
    let randomDate = new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));

    // Format the date as M/D/YYYY without leading zeros
    let day = randomDate.getDate();
    let month = randomDate.getMonth() + 1; // Months are 0-based in JavaScript
    let year = randomDate.getFullYear();

    return `${month}/${day}/${year}`;
}

function getRandomElement(arr) {
    return arr[Math.floor(Math.random() * arr.length)];
}

function getRandomElements(arr, count) {
    const shuffled = arr.sort(() => 0.5 - Math.random());
    return shuffled.slice(0, count);
}

function fillForm() {
    // Generate random values
    _bookTitle = "Test Book " + Math.floor(Math.random() * 1000);
    _bookDescription = "This is a test book description.";
    _bookPublishDate = getRandomDate();

    // Fill title
    cy.get("#Title").type(_bookTitle);

    // Fill description
    cy.get("#Description").type(_bookDescription);

    // Fill publish date
    cy.get("#PublishDate").type(_bookPublishDate);

    // Select random genre
    cy.get("#GenreId").then(($genreDropdown) => {
        const options = $genreDropdown.find("option").not(":first").toArray(); // Exclude the first option
        const randomOption = getRandomElement(options);
        _bookGenre = randomOption.innerText.trim();
        cy.get("#GenreId").select(_bookGenre);
    });

    // Select random authors
    cy.get("#AuthorBooks").then(($authorDropdown) => {
        const options = $authorDropdown.find("option").toArray(); // Include all options
        const randomAuthors = getRandomElements(options, Math.floor(Math.random() * (options.length - 1)) + 1).map(option => option.innerText.trim());
        _bookAuthors = randomAuthors;
        cy.get("#AuthorBooks").select(_bookAuthors);
    });

    // Submit the form
    cy.get("input[type='submit']").click();

    cy.log(`_bookTitle -> ${_bookTitle}`);
    cy.log(`_bookDescription -> ${_bookDescription}`);
    cy.log(`_bookPublishDate -> ${_bookPublishDate}`);
    cy.log(`_bookGenre -> ${_bookGenre}`);
    cy.log(`_bookAuthors -> ${_bookAuthors}`);
}



describe("Testing of book create, update and delete function", () => {

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it("Create new book", () => {
        cy.get("a.nav-link.text-dark[href='/Book']").click();
        cy.get("a.btn.btn-success[href='/Book/Create']").click();
        fillForm();

    });
})