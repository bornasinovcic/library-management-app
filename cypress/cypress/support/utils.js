function getRandomDate() {
    let start = new Date();
    start.setFullYear(start.getFullYear() - 100);
    let end = new Date();
    let randomDate = new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));
    let day = randomDate.getDate();
    let month = randomDate.getMonth() + 1;
    let year = randomDate.getFullYear();
    return `${month}/${day}/${year}`;
}

function getRandomCapitalLetter() {
    let letters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'.split('');
    let excludedLetters = ['M', 'F'];
    let filteredLetters = letters.filter(letter => !excludedLetters.includes(letter));
    let randomIndex = Math.floor(Math.random() * filteredLetters.length);
    return filteredLetters[randomIndex];
}

function getRandomGender() {
    return ['M', 'F'][Math.floor(Math.random() * 2)];
}
function getRandomElements(arr, count) {
    const shuffled = arr.sort(() => 0.5 - Math.random());
    return shuffled.slice(0, count);
}

function getRandomElement(arr) {
    return arr[Math.floor(Math.random() * arr.length)];
}

export default {
    getRandomDate,
    getRandomGender,
    getRandomElement,
    getRandomElements,
    getRandomCapitalLetter
};