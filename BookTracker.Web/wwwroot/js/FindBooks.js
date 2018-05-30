var loopAnimation;

var bookDetailApp = new Vue({
    el: '#book-detail-app',
    data: {
        isDisplayed: false,
        focusedBook: null
    },
    methods: {
        closeDetail: function () {
            this.isDisplayed = false;
            this.focusedBook.isExpanded = false;
        },
        hasPartialStar: function () {
            var intRating = parseInt(this.focusedBook.rating);
            var floatRating = parseFloat(this.focusedBook.rating);

            var remainder = floatRating - intRating;

            if (remainder >= 0.25 && remainder <= 0.75) {
                return true;
            }

            return false;
        }
    }
});

var bookSearchResultsApp = new Vue({
    el: '#book-search-results-app',
    data: {
        results: [],
        isLoading: false,
        hasError: false,
        errorText: ''
    },
    methods: {
        bookClicked: function (result) {
            result.isExpanded = true;
            bookDetailApp.focusedBook = result;
            bookDetailApp.isDisplayed = true;
        }
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
        searchBooks: function () {
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
                    bookSearchResultsApp.errorText = 'Unable to load the requested book';
                },
                complete: function () {
                    bookSearchResultsApp.isLoading = false;
                    loopAnimation.pause();
                    loopAnimation.reset();
                }
            });
        }
    }
});