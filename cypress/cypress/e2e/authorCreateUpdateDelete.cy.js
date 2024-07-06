/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";

describe('Testing of book create, update and delete function', () => {

    let _firstName = '';
    let _lastName = '';
    let _gender = '';
    let _email = '';
    let _dateOfBirth = '';
    let _placeOfBirth = '';
    let _writtenBooks = [];

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it('Create new book', () => {
        cy.get('a.nav-link.text-dark[href="/Author"]').click();
        cy.get('a.btn.btn-success[href="/Writer/Add"]').click();

        let domains = ['hotmail.com', 'gmail.com', 'yahoo.com'];
        let randomDomain = domains[Math.floor(Math.random() * domains.length)];

        let randomNumber = Math.floor(10000 + Math.random() * 90000);
        let firstName = `First${randomNumber}`;
        let lastName = `Last${randomNumber}`;
        let gender = '';
        let email = `email${randomNumber}@${randomDomain}`;
        let dateOfBirth = getRandomDate();
        let placeOfBirth = '';
        let writtenBooks = [];


        randomNumber % 2 === 0 ? gender = 'M' : gender = 'F';


        cy.get('input#FirstName')
            .clear()
            .type(firstName)
            .should('have.value', firstName);
        cy.get('input#LastName')
            .clear()
            .type(lastName)
            .should('have.value', lastName);
        cy.get('input#Gender')
            .clear()
            .type(gender)
            .should('have.value', gender);
        cy.get('input#Email')
            .clear()
            .type(email)
            .should('have.value', email);
        cy.get('input#DateOfBirth')
            .clear()
            .type(dateOfBirth)
            .should('have.value', dateOfBirth);


        cy.get('#PlaceOfBirthId').then($select => {
            let optionsCount = $select.find('option').length;
            let randomIndex = getRandomInt(1, optionsCount - 1); // Skip the first option (index 0)
            cy.get('#PlaceOfBirthId').select($select.find('option').eq(randomIndex).val()).then(() => {
                placeOfBirth = $select.find('option').eq(randomIndex).text(); // Save the selected place of birth text
                cy.log('Selected place of birth:', placeOfBirth);
            });
        });

        cy.get('#AuthorBooks').then($select => {
            let optionsCount = $select.find('option').length;
            let numberOfSelections = getRandomInt(1, optionsCount); // Random number of selections
            let randomIndexes = getRandomIndexes(numberOfSelections, optionsCount); // Get random indexes

            let selectedValues = randomIndexes.map(index => $select.find('option').eq(index).val());
            cy.get('#AuthorBooks').select(selectedValues).then(() => {
                writtenBooks = randomIndexes.map(index => $select.find('option').eq(index).text()); // Save the selected authors text
                cy.log('Selected books:', writtenBooks.join(', '));
            });
        });


    })

})

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

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}

function getRandomIndexes(count, max) {
    let indexes = new Set();
    while (indexes.size < count) {
        indexes.add(getRandomInt(0, max - 1));
    }
    return Array.from(indexes);
}