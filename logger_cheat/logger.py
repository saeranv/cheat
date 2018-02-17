"""
Logging functions in Python
"""

import logging

def basic_logging():
    """
    https://docs.python.org/2/howto/logging.html#logging-basic-tutorial

    # WHEN TO USE LOGGER METHODS
    print()                             # display console output for ordinary usage of cmd line
    logging.info() or logging.debug()   # report events during nomral ops
    warnings.warn()                     # warning regarding runtime event
    Exception                           # report error during runtime event
    logging.error()                     # suppression of error without raise exception

    # LOGGER LEVELS
    DEBUG                               # Detailed information
    INFO                                # Confirm things are working as expected
    WARNING                             # Indication something unexpected has happened
    ERROR                               # Error occurs
    CRITICAL                            # Program may not be able to run
    """

    logging.warning("Watch out!")   # will pring a message to the console
    logging.info("I told you so")   # won't print anything

def log_to_file():
    # Setting logging.DEBUG = all logger levels will be printed to file
    # Setting logging WARNING = only warning logger level will be printed to file
    logging.basicConfig(filename="logger_example.log", filemode='w', level=logging.DEBUG)
    logging.debug("This message is in log file")
    logging.info("And this")
    logging.warning("And this")



if __name__ == "__main__":
    #basic_logging()
    log_to_file()
