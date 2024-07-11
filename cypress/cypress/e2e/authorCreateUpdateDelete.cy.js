/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";
import { getRandomDate, getRandomGender, getRandomElement, getRandomElements } from "../support/utils.js";

let _randomNumber = "";
let _firstName = "";
let _lastName = "";
let _gender = "";
let _email = "";
let _dateOfBirthdate = "";
let _placeOfBirthdate = "";
let _books = [];

function fillForm() {
    cy.wrap(null).then(() => {
        _randomNumber = Math.floor(10000 + Math.random() * 90000);
        _firstName = `First${_randomNumber}`;
        _lastName = `Last${_randomNumber}`;
        _gender = getRandomGender();
        _email = `email${_randomNumber}@gmail.com`;
        _dateOfBirthdate = getRandomDate();
        cy.get("input#FirstName")
            .clear()
            .type(_firstName)
            .should("have.value", _firstName);
        cy.get("input#LastName")
            .clear()
            .type(_lastName)
            .should("have.value", _lastName);
        cy.get("input#Gender")
            .clear()
            .type(_gender)
            .should("have.value", _gender);
        cy.get("input#Email")
            .clear()
            .type(_email)
            .should("have.value", _email);
        cy.get("input#DateOfBirth")
            .clear()
            .type(_dateOfBirthdate)
            .should("have.value", _dateOfBirthdate);
        cy.get("#PlaceOfBirthId").then(($genreDropdown) => {
            const options = $genreDropdown.find("option").not(":first").toArray();
            const randomOption = getRandomElement(options);
            _placeOfBirthdate = randomOption.innerText.trim();
            cy.get("#PlaceOfBirthId").select(_placeOfBirthdate);
        });
        cy.get("#AuthorBooks").then(($authorDropdown) => {
            const options = $authorDropdown.find("option").toArray();
            const randomAuthors = getRandomElements(options, Math.floor(Math.random() * (options.length - 1)) + 1).map(option => option.innerText.trim());
            _books = randomAuthors;
            cy.get("#AuthorBooks").select(_books);
        });
    });
}

function searchAndVerify(testCondition = "defaultCondition") {
    cy.wrap(null).then(() => {
        cy.get("input#FullName")
            .clear()
            .type(`${_firstName} ${_lastName}`)
            .should("have.value", `${_firstName} ${_lastName}`);
        cy.get("input#Email")
            .clear()
            .type(_email)
            .should("have.value", _email);
        cy.get("input#City")
            .clear()
            .type(_placeOfBirthdate)
            .should("have.value", _placeOfBirthdate);
        cy.get("button.btn.btn-outline-info").click();
        if (testCondition === "create" || testCondition === "update") {
            cy.get("tr").contains(`${_firstName} ${_lastName}`).click();
            cy.get("dd#firstName").should("contain", _firstName);
            cy.get("dd#lastName").should("contain", _lastName);
            cy.get("dd#gender").should("contain", _gender);
            cy.get("dd#email").should("contain", _email);
            cy.get("dd#dateOfBirth").should("contain", _dateOfBirthdate);
            cy.get("dd#city").should("contain", _placeOfBirthdate);
            cy.get('#books ul li a').then($links => {
                let books = [...$links].map(link => link.innerText.trim());
                books.sort();
                _books.sort();
                expect(books.length).to.equal(books.length);
                books.forEach((book, index) => {
                    expect(book).to.equal(_books[index]);
                });
            });
        } else if (testCondition === "delete") {
            cy.get(".table.table-condensed tbody").find("tr").should("have.length", 0);
        }
    });
}

describe("Testing of author create, update and delete function", () => {

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it("Create new author", () => {
        cy.get("a.nav-link.text-dark[href='/Author']").click();
        cy.get("a.btn.btn-success[href='/Writer/Add']").click();
        fillForm();
        cy.get("input[type='submit'][value='Create'].btn.btn-outline-success.mt-2").click();
        searchAndVerify("create");
    });

    it("Update already existing author", () => {
        cy.get("a.nav-link.text-dark[href='/Author']").click();
        searchAndVerify();
        cy.contains("tr", `${_firstName} ${_lastName}`).within(() => {
            cy.get("a.btn.btn-primary").click();
        });
        fillForm();
        cy.get("input[type='submit'][value='Save edit'].btn.btn-outline-success.mt-2").click();
        searchAndVerify("update");
    });

    it("Delete already existing author", () => {
        cy.get("a.nav-link.text-dark[href='/Author']").click();
        searchAndVerify()
        cy.contains("tr", `${_firstName} ${_lastName}`).within(() => {
            cy.get("button.btn.btn-danger").click();
        });
        searchAndVerify("delete");
    });
})