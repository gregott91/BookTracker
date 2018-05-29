var loopAnimation;

function searchBooks() {
    if (bookSearchApp.bookSearchQuery.length == 0) {
        return;
    }

    bookSearchResultsApp.isLoading = true;

    $.ajax({
        url: '/Book/SearchBooksAsync?searchQuery=' + bookSearchApp.bookSearchQuery,
        type: 'GET',
        success: function (data) {
            bookSearchResultsApp.hasError = false;
            bookSearchResultsApp.results = data;
        },
        error: function (data) {
            bookSearchResultsApp.hasError = true;
            bookSearchResultsApp.errorText = 'Unable to load the requested show';
        },
        complete: function () {
            bookSearchResultsApp.isLoading = false;
            loopAnimation.pause();
            loopAnimation.reset();
        }
    });
}

var bookSearchResultsApp = new Vue({
    el: '#book-search-results-app',
    data: {
        results: [],
        isLoading: false,
        hasError: false,
        errorText: ''
    },
    updated: function () {
        loopAnimation = anime({
            targets: '#loading-indicator',
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