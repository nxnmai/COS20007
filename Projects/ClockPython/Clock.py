from Counter import Counter

class Clock:
    def __init__(self, is_24_hour_format=None):
        if is_24_hour_format is None:
            student_id = input("Please enter your student ID: ")
            if not student_id:
                raise ValueError("Student ID cannot be null or empty.")
            last_digit = int(student_id) % 10
            self._is_24_hour_format = last_digit > 5
        else:
            self._is_24_hour_format = is_24_hour_format

        self._hours = Counter("hours")
        self._minutes = Counter("minutes")
        self._seconds = Counter("seconds")

    def Tick(self):
        self._seconds.Increment()
        if self._seconds.Ticks >= 60:
            self._seconds.Reset()
            self._minutes.Increment()
            if self._minutes.Ticks >= 60:
                self._minutes.Reset()
                self._hours.Increment()
                max_hours = 24
                if self._hours.Ticks >= max_hours:
                    self._hours.Reset()

    def Reset(self):
        self._hours.Reset()
        self._minutes.Reset()
        self._seconds.Reset()

    @property
    def Time(self):
        if self._is_24_hour_format:
            return f"{self._hours.Ticks:02d}:{self._minutes.Ticks:02d}:{self._seconds.Ticks:02d}"
        else:
            display_hours = self._hours.Ticks % 12
            if display_hours == 0:
                display_hours = 12
            period = "AM" if self._hours.Ticks < 12 else "PM"
            return f"{display_hours:02d}:{self._minutes.Ticks:02d}:{self._seconds.Ticks:02d} {period}"

    @property
    def Hours(self):
        return self._hours.Ticks

    @property
    def Minutes(self):
        return self._minutes.Ticks

    @property
    def Seconds(self):
        return self._seconds.Ticks
    
