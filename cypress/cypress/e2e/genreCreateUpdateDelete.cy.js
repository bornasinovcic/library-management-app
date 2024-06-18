/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";

describe('Testing of genre create, update and delete function', () => {

    let _genreName = '';

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it('Create new genre', () => {
        cy.get('a.nav-link.text-dark[href="/Genre"]').click();
        cy.get('a.btn.btn-success[href="/Genre/Create"]').click();

        let randomNumber = Math.floor(10000 + Math.random() * 90000);
        let genreName = `Genre${randomNumber}`;

        _genreName = genreName;
        cy.log(`The genreName saved is: ${_genreName}`);

        cy.get('input#Name')
            .clear()
            .type(genreName)
            .should('have.value', genreName);

        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();
    });

    it('Update already existing genre', () => {
        cy.get('a.nav-link.text-dark[href="/Genre"]').click();

        cy.get('input#Name')
            .clear()
            .type(_genreName)
            .should('have.value', _genreName);

        cy.get('button.btn.btn-outline-info').click();

        cy.contains('tr', _genreName).within(() => {
            cy.get('a.btn.btn-primary').click();
        });

        let randomNumber = Math.floor(10000 + Math.random() * 90000);
        let genreName = `Genre${randomNumber}`;

        _genreName = genreName;
        cy.log(`The genreName saved is: ${_genreName}`);

        cy.get('input#Name')
            .clear()
            .type(genreName)
            .should('have.value', genreName);

        cy.get('input[type="submit"][value="Save edit"].btn.btn-outline-success.mt-2').click();

    });

    it('Delete already existing genre', () => {
        cy.get('a.nav-link.text-dark[href="/Genre"]').click();

        cy.get('input#Name')
            .clear()
            .type(_genreName)
            .should('have.value', _genreName);

        cy.get('button.btn.btn-outline-info').click();

        cy.contains('tr', _genreName).within(() => {
            cy.get('button.btn.btn-danger').click();
        });

    });

});