var loopAnimation;

var bookDetailApp = new Vue({
    el: '#book-detail-app',
    data: {
        focusedBook: {},
        isDisplayed: false,
        isDescriptionExpanded: false,
        isLoading: false
    },
    methods: {
        openDetail: function (result) {
            this.focusedBook = result;
            this.resetActionButtons();
            this.isDisplayed = true;
            this.isLoading = true;
            this.loadBookActions();
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
        resetActionButtons: function() {
            this.resetActionButton('#add-to-button', '#add-to-sub-buttons-upper', '#add-to-sub-buttons-lower');
            this.resetActionButton('#mark-as-button', '#mark-as-sub-buttons-upper', '#mark-as-sub-buttons-lower');
        },
        resetActionButton: function(button, upperSubButtons, lowerSubButtons) {
            $(button).show();
            $(button).removeClass('expanded-detail-button');
            $(upperSubButtons).hide();
            $(lowerSubButtons).hide();
        },
        loadBookActions: function () {
            $.ajax({
                url: '/Book/GetUserBookProperties?isbn=' + this.focusedBook.isbn,
                type: 'GET',
                success: function (data) {
                    
                },
                error: function (data) {
                    
                },
                complete: function () {
                    this.isLoading = false;
                }
            });
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
            bookDetailApp.openDetail(result);
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
            if ((!bookSearchApp.bookSearchQuery) || bookSearchApp.bookSearchQuery.length === 0) {
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