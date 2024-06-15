describe('Testing of genre create, update and delete functions', () => {

    let genreName = '';

    beforeEach(() => {
        cy.visit('https://localhost:7065/');
    });

    it('Create new genre', () => {
        cy.get('a.nav-link.text-dark[href="/Genre"]').click();
        cy.get('a.btn.btn-success[href="/Genre/Create"]').click();

        const randomNumber = Math.floor(10000 + Math.random() * 90000);
        const name = `Genre${randomNumber}`;

        genreName = name;
        cy.log(`The genreName saved is: ${genreName}`);

        cy.get('input#Name')
            .clear()
            .type(name)
            .should('have.value', name);

        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();
    });

    it('Update already existing genre', () => {
        cy.get('a.nav-link.text-dark[href="/Genre"]').click();

        cy.get('input#Name')
            .clear()
            .type(genreName)
            .should('have.value', genreName);

        cy.get('button.btn.btn-outline-info').click();

        cy.contains('tr', genreName).within(() => {
            cy.get('a.btn.btn-primary').click();
        });

        const randomNumber = Math.floor(10000 + Math.random() * 90000);
        const name = `Genre${randomNumber}`;

        genreName = name;
        cy.log(`The genreName saved is: ${genreName}`);

        cy.get('input#Name')
            .clear()
            .type(name)
            .should('have.value', name);

        cy.get('input[type="submit"][value="Save edit"].btn.btn-outline-success.mt-2').click();

    });

    it('Delete already existing genre', () => {
        cy.get('a.nav-link.text-dark[href="/Genre"]').click();

        cy.get('input#Name')
            .clear()
            .type(genreName)
            .should('have.value', genreName);

        cy.get('button.btn.btn-outline-info').click();

        cy.contains('tr', genreName).within(() => {
            cy.get('button.btn.btn-danger').click();
        });

    });

});