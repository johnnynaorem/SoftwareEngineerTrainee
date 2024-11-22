<template>
    <div class="carousel-container">
        <div class="carousel" :style="carouselStyle">
            <div class="carousel-card" v-for="(card, index) in visibleCards" :key="index">
                <div class="card">
                    <img :src="card.imgSrc" alt="card image" class="card-image" />
                    <div class="card-body">
                        <h3>{{ card.title }}</h3>
                        <p>{{ card.description }}</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Dot Navigation -->
        <div class="dots-container">
            <span v-for="(group, index) in totalGroups" :key="index"
                :class="['dot', { active: index === currentIndex }]" @click="setIndex(index)"></span>
        </div>
    </div>
</template>

<script>
export default {
    name: "TesTing",
    data() {
        return {
            currentIndex: 0,
            cards: [
                { imgSrc: 'https://via.placeholder.com/300x200', title: 'Card 1', description: 'This is card 1' },
                { imgSrc: 'https://via.placeholder.com/300x200', title: 'Card 2', description: 'This is card 2' },
                { imgSrc: 'https://via.placeholder.com/300x200', title: 'Card 3', description: 'This is card 3' },
                { imgSrc: 'https://via.placeholder.com/300x200', title: 'Card 4', description: 'This is card 4' },
                { imgSrc: 'https://via.placeholder.com/300x200', title: 'Card 5', description: 'This is card 5' },
                { imgSrc: 'https://via.placeholder.com/300x200', title: 'Card 6', description: 'This is card 6' },
                { imgSrc: 'https://via.placeholder.com/300x200', title: 'Card 7', description: 'This is card 7' },
            ],
            cardsPerPage: 3, // Number of cards to show at once
        };
    },
    computed: {
        // The total number of groups (dots) is the total number of cards divided by cardsPerPage
        totalGroups() {
            return Math.ceil(this.cards.length / this.cardsPerPage);
        },
        // Determine the cards that should be visible based on the current index and the number of cards per page
        visibleCards() {
            const start = this.currentIndex * this.cardsPerPage;
            return this.cards.slice(start, start + this.cardsPerPage);
        },
        carouselStyle() {
            return {
                transform: `translateX(-${this.currentIndex * 320}px)`, // Adjust for cardsPerPage
                transition: 'transform 0.5s ease',
            };
        },
    },
    methods: {
        setIndex(index) {
            this.currentIndex = index;
        },
    },
};
</script>

<style scoped>
.carousel-container {
    position: relative;
    width: 100%;
    /* Adjust to the width you need */
    max-width: 960px;
    /* Adjust max-width if needed */
    overflow: hidden;
}

.carousel {
    display: flex;
    transition: transform 0.5s ease;
}

.carousel-card {
    flex: 0 0 calc(100% / 3 - 20px);
    /* Show 3 cards per slide, with spacing */
    margin-right: 20px;
    /* Space between the cards */
}

.card {
    width: 100%;
    background-color: #fff;
    border-radius: 10px;
    overflow: hidden;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.card-image {
    width: 100%;
    height: 200px;
    object-fit: cover;
}

.card-body {
    padding: 16px;
}

.card-body h3 {
    margin-top: 0;
}

.dots-container {
    display: flex;
    justify-content: center;
    margin-top: 10px;
}

.dot {
    width: 10px;
    height: 10px;
    margin: 0 5px;
    background-color: #ccc;
    border-radius: 50%;
    cursor: pointer;
    transition: background-color 0.3s;
}

.dot.active {
    background-color: #333;
}

.dot:hover {
    background-color: #666;
}
</style>