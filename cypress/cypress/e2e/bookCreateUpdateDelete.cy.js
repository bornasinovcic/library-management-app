/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";
import { getRandomDate, getRandomElement, getRandomElements } from "../support/utils.js";

let _randomNumber = "";
let _bookTitle = "";
let _bookDescription = "";
let _bookPublishDate = "";
let _bookGenre = "";
let _bookAuthors = [];

function fillForm() {
    cy.wrap(null).then(() => {
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
        cy.get("input#PublishDate")
            .clear()
            .type(_bookPublishDate)
            .should("have.value", _bookPublishDate);
        cy.get("#GenreId").then(($genreDropdown) => {
            const options = $genreDropdown.find("option").not(":first").toArray();
            const randomOption = getRandomElement(options);
            _bookGenre = randomOption.innerText.trim();
            cy.get("#GenreId").select(_bookGenre);
        });
        cy.get("#AuthorBooks").then(($authorDropdown) => {
            const options = $authorDropdown.find("option").toArray();
            const randomAuthors = getRandomElements(options, Math.floor(Math.random() * (options.length - 1)) + 1).map(option => option.innerText.trim());
            _bookAuthors = randomAuthors;
            cy.get("#AuthorBooks").select(_bookAuthors);
        });
    });
}

function searchAndVerify(testCondition = "defaultCondition") {
    cy.wrap(null).then(() => {
        cy.get("input#Title")
            .clear()
            .type(_bookTitle)
            .should("have.value", _bookTitle);
        cy.get("input#Description")
            .clear()
            .type(_bookDescription)
            .should("have.value", _bookDescription);
        cy.get("input#Genre")
            .clear()
            .type(_bookGenre)
            .should("have.value", _bookGenre);
        cy.get("button.btn.btn-outline-info").click();
        if (testCondition === "create" || testCondition === "update") {
            cy.get("tr").contains(_bookTitle).click();
            cy.get("dd#title").should("contain", _bookTitle);
            cy.get("dd#description").should("contain", _bookDescription);
            cy.get("dd#publishDate").should("contain", _bookPublishDate);
            cy.get("dd#bookGenre").should("contain", _bookGenre);
            cy.get('#authors ul li a').then($links => {
                let bookAuthors = [...$links].map(link => link.innerText.trim());
                bookAuthors.sort();
                _bookAuthors.sort();
                expect(bookAuthors.length).to.equal(_bookAuthors.length);
                bookAuthors.forEach((author, index) => {
                    expect(author).to.equal(_bookAuthors[index]);
                });
            });
        } else if (testCondition === "delete") {
            cy.get(".table.table-condensed tbody").find("tr").should("have.length", 0);
        }
    });
}

describe("Testing of book create, update and delete function", () => {

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it("Create new book", () => {
        cy.get("a.nav-link.text-dark[href='/Book']").click();
        cy.get("a.btn.btn-success[href='/Book/Create']").click();
        fillForm();
        cy.get("input[type='submit'][value='Create'].btn.btn-outline-success.mt-2").click();
        searchAndVerify("create");
    });

    it("Update already existing book", () => {
        cy.get("a.nav-link.text-dark[href='/Book']").click();
        searchAndVerify();
        cy.contains("tr", _bookTitle).within(() => {
            cy.get("a.btn.btn-primary").click();
        });
        fillForm();
        cy.get("input[type='submit'][value='Save edit'].btn.btn-outline-success.mt-2").click();
        searchAndVerify("update");
    });

    it("Delete already existing book", () => {
        cy.get("a.nav-link.text-dark[href='/Book']").click();
        searchAndVerify();
        cy.contains("tr", _bookTitle).within(() => {
            cy.get("button.btn.btn-danger").click();
        });
        searchAndVerify("delete");
    });
})