function searchBooks() {
    bookSearchResultsApp.isLoading = true;

    $.get('/Book/SearchBooksAsync?searchQuery=' + bookSearchApp.bookSearchQuery, function (data, status) {
        bookSearchResultsApp.isLoading = false;

        bookSearchResultsApp.results = data;
    });
}

var bookSearchResultsApp = new Vue({
    el: '#book-search-results-app',
    data: {
        results: [],
        isLoading: false
    },
    updated: function () {
        anime({
            targets: '.loading-indicator',
            translateX: 0,
            rotate: 360,
            easing: 'easeInOutQuad',
            loop: true
        });

        $('.book-result-description').dotdotdot();
    }
});

var bookSearchApp = new Vue({
    el: '#book-search-app',
    data: {
        bookSearchQuery: ''
    },
    methods: {
        searchBooks: searchBooks
    }
});