var loopAnimation;

var bookDetailApp = new Vue({
    el: '#book-detail-app',
    data: {
        focusedBook: {},
        isDisplayed: false,
        isDescriptionExpanded: false,
    },
    methods: {
        openDetail: function () {
            this.resetActionButtons();
            this.isDisplayed = true;
        },
        closeDetail: function () {
            this.isDisplayed = false;
            this.focusedBook.isExpanded = false;
            this.isDescriptionExpanded = false;
        },
        hasPartialStar: function () {
            if (!this.focusedBook) {
                return;
            }

            var intRating = parseInt(this.focusedBook.rating);
            var floatRating = parseFloat(this.focusedBook.rating);

            var remainder = floatRating - intRating;

            if (remainder >= 0.25 && remainder <= 0.75) {
                return true;
            }

            return false;
        },
        expandDescription: function () {
            this.isDescriptionExpanded = true;
        },
        collapseDescription: function () {
            this.isDescriptionExpanded = false;
        },
        changeMarkAsMenu: function () {
            this.changeActionButtons('#mark-as-button', '#add-to-button', '#mark-as-sub-buttons-upper', '#mark-as-sub-buttons-lower');
        },
        changeAddToMenu: function () {
            this.changeActionButtons('#add-to-button', '#mark-as-button', '#add-to-sub-buttons-upper', '#add-to-sub-buttons-lower');
        },
        changeActionButtons: function (primaryButton, secondaryButton, upperSubButtons, lowerSubButtons) {
            if ($(secondaryButton).is(":visible")) {
                $(primaryButton).addClass('expanded-detail-button');
                $(secondaryButton).hide();
                $(upperSubButtons).css('display', 'inline-block');
                $(lowerSubButtons).show('slide', { direction: 'left' }, 300);
            } else {
                $(lowerSubButtons).hide('slide', {
                    direction: 'left', complete: function () {
                        $(upperSubButtons).css('display', 'none');
                        $(secondaryButton).show();
                        $(primaryButton).removeClass('expanded-detail-button');
                    }
                }, 300);
            }
        },
        resetActionButtons() {
            this.resetActionButton('#add-to-button', '#add-to-sub-buttons-upper', '#add-to-sub-buttons-lower');
            this.resetActionButton('#mark-as-button', '#mark-as-sub-buttons-upper', '#mark-as-sub-buttons-lower');
        },
        resetActionButton(button, upperSubButtons, lowerSubButtons) {
            $(button).show();
            $(button).removeClass('expanded-detail-button');
            $(upperSubButtons).hide();
            $(lowerSubButtons).hide();
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
            bookDetailApp.openDetail();
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
            if ((!bookSearchApp.bookSearchQuery) || bookSearchApp.bookSearchQuery.length == 0) {
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