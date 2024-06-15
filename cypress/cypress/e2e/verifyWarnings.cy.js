describe('Verify warnings for required parameters', () => {

  beforeEach(() => {
    cy.visit('https://localhost:7065/');
  });

  function getRandomCapitalLetter() {
    const letters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'.split('');
    const excludedLetters = ['M', 'F'];
    const filteredLetters = letters.filter(letter => !excludedLetters.includes(letter));
    const randomIndex = Math.floor(Math.random() * filteredLetters.length);
    return filteredLetters[randomIndex];
  }

  it('Verify warning for required parameters in author creation', () => {
    const randomLetter = getRandomCapitalLetter();
    cy.get('a.nav-link.text-dark[href="/Author"]').click();
    cy.get('a.btn.btn-success[href="/Writer/Add"]').click();
    cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

    cy.get('span.text-danger.field-validation-error[data-valmsg-for="FirstName"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'First name is required.');
    cy.get('span.text-danger.field-validation-error[data-valmsg-for="LastName"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'Last name is required.');
    cy.get('span.text-danger.field-validation-error[data-valmsg-for="Gender"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', "The value '' is invalid.");
    cy.get('span.text-danger.field-validation-error[data-valmsg-for="Email"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'Email address is required.');
    cy.get('span.text-danger.field-validation-error[data-valmsg-for="DateOfBirth"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', "The value '' is invalid.");

    cy.get('input#Gender').clear().type(randomLetter);
    cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();
    cy.get('span.text-danger.field-validation-error[data-valmsg-for="Gender"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', "Gender must be 'M' or 'F'.");
  });

  it('Verify warning for required parameters in book creation', () => {
    cy.get('a.nav-link.text-dark[href="/Book"]').click();
    cy.get('a.btn.btn-success[href="/Book/Create"]').click();
    cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

    cy.get('span.text-danger.field-validation-error[data-valmsg-for="Title"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'Title is required.');
    cy.get('span.text-danger.field-validation-error[data-valmsg-for="Description"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'Description is required.');
    cy.get('span.text-danger.field-validation-error[data-valmsg-for="PublishDate"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'The value \'\' is invalid');
  });

  it('Verify warning for required parameters in city creation', () => {
    cy.get('a.nav-link.text-dark[href="/City"]').click();
    cy.get('a.btn.btn-success[href="/Place/Add"]').click();
    cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

    cy.get('span.text-danger.field-validation-error[data-valmsg-for="Name"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'City name is required.');
    cy.get('span.text-danger.field-validation-error[data-valmsg-for="Country"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'Country name is required.');
  });

  it('Verify warning for required parameters in genre ceation', () => {
    cy.get('a.nav-link.text-dark[href="/Genre"]').click();
    cy.get('a.btn.btn-success[href="/Genre/Create"]').click();
    cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

    cy.get('span.text-danger.field-validation-error[data-valmsg-for="Name"][data-valmsg-replace="true"]')
      .should('be.visible')
      .and('contain', 'Genre name is required.');
  });

})