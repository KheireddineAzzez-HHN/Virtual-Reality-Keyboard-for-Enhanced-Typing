import matplotlib.pyplot as plt
import numpy as np
import string

# Define the base frequency for the click sound
base_frequency = 440  # Example base frequency in Hz (A4)

# Function to calculate the new frequency after pitch shift
def calculate_frequency(base_frequency, semitones):
    return base_frequency * (2 ** (semitones / 12.0))

# Generate frequencies for each letter key
frequencies = {}
for i, char in enumerate(string.ascii_lowercase):
    semitones = i - 13  # Shift from -13 semitones for 'a' to +12 semitones for 'z'
    frequencies[char] = calculate_frequency(base_frequency, semitones)

# Plot the frequencies
keys = list(frequencies.keys())
values = list(frequencies.values())

plt.figure(figsize=(10, 6))
plt.plot(keys, values, marker='o')
plt.xlabel('Keys')
plt.ylabel('Frequency (Hz)')
plt.title('Frequency Changes Across Keys')
plt.grid(True)
plt.savefig('export/frequency_changes.png')
plt.show()
semitones = [i - 13 for i in range(26)]  # From -13 for 'a' to +12 for 'z'

plt.figure(figsize=(10, 6))
plt.plot(keys, semitones, marker='o')
plt.xlabel('Keys')
plt.ylabel('Semitone Shift')
plt.title('Semitone Shifts Across Keys')
plt.grid(True)
plt.savefig('export/semitone_shifts.png')
plt.show()


import numpy as np
import matplotlib.pyplot as plt
from pydub import AudioSegment
import string

# Function to generate waveform data
def generate_waveform(sound, sample_rate=44100):
    # Get raw audio data
    raw_data = np.array(sound.get_array_of_samples())
    # Normalize data
    waveform = raw_data / (2**15)
    # Generate time axis
    t = np.linspace(0, len(waveform) / sample_rate, num=len(waveform))
    return t, waveform

# Define the letters and their corresponding semitone shifts
letters = ['a', 'h', 'o', 'z']
semitone_shifts = [string.ascii_lowercase.index(letter) - 13 for letter in letters]

# Load original sound
original_sound = AudioSegment.from_file("import/audio/click_segment.mp3")  # Duration 200 ms

# Create a figure with subplots
fig, axes = plt.subplots(2, 2, figsize=(14, 10))
axes = axes.flatten()

# Generate and plot waveforms for each letter
for i, (letter, semitone_shift) in enumerate(zip(letters, semitone_shifts)):
    # Apply pitch shift for each letter
    shifted_sound = original_sound._spawn(original_sound.raw_data, overrides={
        "frame_rate": int(original_sound.frame_rate * (2.0 ** (semitone_shift / 12.0)))
    }).set_frame_rate(original_sound.frame_rate)
    
    # Generate waveforms
    t_original, waveform_original = generate_waveform(original_sound)
    t_shifted, waveform_shifted = generate_waveform(shifted_sound)

    # Plot waveforms
    axes[i].plot(t_original, waveform_original, label='Original Waveform ')
    axes[i].plot(t_shifted, waveform_shifted, label=f'Shifted Waveform for \'{letter}', linestyle='--')
    axes[i].set_xlabel('Time (s)')
    axes[i].set_ylabel('Amplitude')
    axes[i].set_title(f'Waveform Comparison for \'{letter}\'')
    axes[i].legend()
    axes[i].grid(True)

# Adjust layout and save the figure
plt.tight_layout()
plt.savefig('waveform_comparison_combined.png')
plt.show()
