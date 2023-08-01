// Getting slider elements.
const slider = document.querySelector('.slider');
const dots = document.querySelectorAll('.dot');
const headerContainer = document.querySelector('.header_main_conteiner');

// Setting active slide and dot.
let activeSlide = 0;
let activeDot = dots[activeSlide];
let timer;

// Function for switching to a specified slide.
function goToSlide(index) {
    // Checking the valid slide index.
    if (index < 0 || index >= dots.length) return;

    // Removing active classes from the previous slide and dot.
    dots[activeSlide].classList.remove('active');

    // Adding active classes to the current slide and dot.
    dots[index].classList.add('active');

    // Updating the active slide and dot
    activeSlide = index;
    activeDot = dots[activeSlide];

    // Setting the background image for the container.
    headerContainer.style.backgroundImage = `url('/images/slide${index + 1}.jpg')`;

    // Resetting the timer and starting a new one.
    clearInterval(timer);
    timer = setInterval(nextSlide, 5000);
}

// Function for switching to the next slide.
function nextSlide() {
    const nextSlideIndex = (activeSlide + 1) % dots.length;
    goToSlide(nextSlideIndex);
}

// Slider dot click handler.
function handleDotClick(event) {
    // Getting the index of the clicked dot.
    const clickedDot = event.target;
    const index = Array.from(dots).indexOf(clickedDot);

    // Switching to the corresponding slide
    goToSlide(index);
}

// Adding a click handler for each dot.
dots.forEach(dot => {
    dot.addEventListener('click', handleDotClick);
});

// Starting the slider with slide 0.
goToSlide(0);

// Starting automatic slide change.
timer = setInterval(nextSlide, 5000);