// Flocking
// Originally developed by Daniel Shiffman - https://thecodingtrain.com/CodingChallenges/124-flocking-boids.html
// Modified and optimized by Ryan Darras

class Boid {
  constructor(state) {
    this.position = createVector(random(width), random(height));
    this.velocity = p5.Vector.random2D();
    this.velocity.setMag(random(2, 4));
    this.acceleration = createVector();
    this.maxForce = 0.2;
    this.maxSpeed = 5;
	this.boidsToConsider = [];
	this.perceptionRadius = 25;
	this.tempPosition = createVector();
	this.tempDistance = 0;
  }

  edges() {
    if (this.position.x > width) {
      this.position.x = 0;
    } else if (this.position.x < 0) {
      this.position.x = width;
    }
    if (this.position.y > height) {
      this.position.y = 0;
    } else if (this.position.y < 0) {
      this.position.y = height;
    }
  }
  
  findboidsToConsider(flock){
		this.boidsToConsider = []
		for (let other of flock) {
			if (other != this)
			{
				let d = this.distance(other);
				other.tempDistance = d;
				if (d < this.perceptionRadius) {
					this.boidsToConsider.push(other);
				}
			}

		}
  }

	distance(other){
		let tempx = other.position.x;
		let tempy = other.position.y;
		if (Math.abs(this.position.x - other.position.x) >= width - perceptionRadiusSlider.value())
		{
			//Need to consider X direction
			tempx = this.convertX(other);
			other.tempPosition.x = tempx;
		}
		if (Math.abs(this.position.y - other.position.y) >= height - perceptionRadiusSlider.value())
		{
			//Need to consider Y direction
			tempy = this.convertY(other);
			other.tempPosition.y = tempy;
		}

		return dist(this.position.x, this.position.y, tempx, tempy);
	}
	
	convertX(other){
		if (other.position.x >= width - perceptionRadiusSlider.value())
		{
			return 0 - width + other.position.x;
		}
		else
		{
			return width + other.position.x;
		}
	}
	
	convertY(other){
		if (other.position.y >= height - perceptionRadiusSlider.value())
		{
			return 0 - height + other.position.y;
		}
		else
		{
			return height + other.position.y;
		}
	}

	//Good
  align(flock) {
    let steering = createVector();
    let total = 0;
    for (let other of flock) {
		steering.add(other.velocity);
		total++;
    }
    if (total > 0) {
      steering.div(total);
      steering.setMag(this.maxSpeed);
      steering.sub(this.velocity);
      steering.limit(this.maxForce);
    }
    return steering;
  }

  //good
  separation(flock) {
    let steering = createVector();
    let total = 0;
    for (let other of flock) {
		let d = other.tempDistance;
		let diff = p5.Vector.sub(this.position, other.tempPosition);
        diff.div(d * d);
        steering.add(diff);
		total++;
    }
    if (total > 0) {
      steering.div(total);
      steering.setMag(this.maxSpeed);
      steering.sub(this.velocity);
      steering.limit(this.maxForce);
    }
    return steering;
  }

  cohesion(flock) {
    let steering = createVector();
    let total = 0;
    for (let other of flock) {
      steering.add(other.tempPosition);
	  total++;
    }
    if (total > 0) {
      steering.div(total);
      steering.sub(this.position);
      steering.setMag(this.maxSpeed);
      steering.sub(this.velocity);
      steering.limit(this.maxForce);
    }
    return steering;
  }

  flock(flock) {
	//handles the blow code directly instead of creating temp array and returning
	//let boidsToConsider = this.findboidsToConsider(states)
    for (let other of this.boidsToConsider)
	{
		other.tempPosition = createVector(other.position.x, other.position.y);
	}
	this.findboidsToConsider(flock)
    let alignment = this.align(this.boidsToConsider);
    let cohesion = this.cohesion(this.boidsToConsider);
    let separation = this.separation(this.boidsToConsider);

    alignment.mult(alignSlider.value());
    cohesion.mult(cohesionSlider.value());
    separation.mult(separationSlider.value());
	
	this.maxForce = maxForceSlider.value();
    this.maxSpeed = maxSpeedSlider.value();
	this.perceptionRadius = perceptionRadiusSlider.value();

    this.acceleration.add(alignment);
    this.acceleration.add(cohesion);
    this.acceleration.add(separation);
	

  }

  update() {
	this.position.add(this.velocity);
    this.velocity.add(this.acceleration);
    this.velocity.limit(this.maxSpeed);
	
    this.acceleration.mult(0);
  }

  show() {
    strokeWeight(6);
    stroke(255);
    point(this.position.x, this.position.y);
  }
}