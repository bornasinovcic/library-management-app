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

function searchAndVerify() {

}

function fillForm() {
    _randomNumber = Math.floor(10000 + Math.random() * 90000);
    _bookTitle = `Title${_randomNumber}`;
    _bookDescription = `Description${_randomNumber}`;
    _bookPublishDate = getRandomDate();

    cy.get("input#Title")
        .clear()
        .type(_bookTitle)
        .should("have.value", _bookTitle);
    cy.get("input#Description")
        .clear()
        .type(_bookDescription)
        .should("have.value", _bookDescription);
    cy.get("#PublishDate")
        .clear()
        .type(_bookPublishDate)
        .should("have.value", _bookPublishDate);

}

describe("Testing of book create, update and delete function", () => {

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it("Create new book", () => {
        cy.get("a.nav-link.text-dark[href='/Book']").click();
        cy.get("a.btn.btn-success[href='/Book/Create']").click();
        fillForm();
        // cy.get("input[type='submit'][value='Create'].btn.btn-outline-success.mt-2").click();
        searchAndVerify("create");
    });
})