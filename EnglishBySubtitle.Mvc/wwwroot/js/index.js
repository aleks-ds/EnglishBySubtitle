// Получаем элементы слайдера
const slider = document.querySelector('.slider');
const dots = document.querySelectorAll('.dot');
const headerContainer = document.querySelector('.header_main_conteiner');

// Устанавливаем активный слайд и точку
let activeSlide = 0;
let activeDot = dots[activeSlide];
let timer;

// Функция для переключения на заданный слайд
function goToSlide(index) {
    // Проверяем допустимый индекс слайда
    if (index < 0 || index >= dots.length) return;

    // Удаляем активные классы у предыдущего слайда и точки
    dots[activeSlide].classList.remove('active');

    // Добавляем активные классы к текущему слайду и точке
    dots[index].classList.add('active');

    // Обновляем активный слайд и точку
    activeSlide = index;
    activeDot = dots[activeSlide];

    // Устанавливаем фоновое изображение для контейнера
    headerContainer.style.backgroundImage = `url('/images/slide${index + 1}.jpg')`;

    // Сбрасываем таймер и запускаем новый
    clearInterval(timer);
    timer = setInterval(nextSlide, 5000);
}

// Функция для переключения на следующий слайд
function nextSlide() {
    const nextSlideIndex = (activeSlide + 1) % dots.length;
    goToSlide(nextSlideIndex);
}

// Обработчик клика по точке слайдера
function handleDotClick(event) {
    // Получаем индекс кликнутой точки
    const clickedDot = event.target;
    const index = Array.from(dots).indexOf(clickedDot);

    // Переключаемся на соответствующий слайд
    goToSlide(index);
}

// Добавляем обработчик клика для каждой точки
dots.forEach(dot => {
    dot.addEventListener('click', handleDotClick);
});

// Запускаем слайдер со слайдом 0
goToSlide(0);

// Запускаем автоматическую смену слайдов
timer = setInterval(nextSlide, 5000);