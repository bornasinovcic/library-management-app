/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";

describe('Testing of genre create, update and delete function', () => {

    let _randomNumber = "";
    let _genreName = "";
    let testCondition = "";

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    afterEach(() => {
        cy.get('input#Name')
            .clear()
            .type(_genreName)
            .should('have.value', _genreName);
        cy.get('button.btn.btn-outline-info').click();
        if (testCondition === 'create' || testCondition === 'update') {
            cy.get('tr').contains(_genreName).click();
            cy.get('dd#name').should('contain', _genreName);
        } else if (testCondition === 'delete') {
            cy.get('.table.table-condensed tbody').find('tr').should('have.length', 0)
        }
    });

    it('Create new genre', () => {
        cy.get('a.nav-link.text-dark[href="/Genre"]').click();
        cy.get('a.btn.btn-success[href="/Genre/Create"]').click();
        _randomNumber = Math.floor(10000 + Math.random() * 90000);
        _genreName = `Genre${_randomNumber}`;
        cy.get('input#Name')
            .clear()
            .type(_genreName)
            .should('have.value', _genreName);
        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();
        testCondition = "create"
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
        _randomNumber = Math.floor(10000 + Math.random() * 90000);
        _genreName = `Genre${_randomNumber}`;
        cy.get('input#Name')
            .clear()
            .type(_genreName)
            .should('have.value', _genreName);

        cy.get('input[type="submit"][value="Save edit"].btn.btn-outline-success.mt-2').click();
        testCondition = 'update'
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
        testCondition = 'delete';
    });
    
});